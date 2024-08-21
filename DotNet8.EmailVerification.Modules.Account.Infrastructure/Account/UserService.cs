﻿using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.DTOs.Features.Setup;
using DotNet8.EmailVerification.Modules.Account.Domain.Account;
using DotNet8.EmailVerification.Modules.Account.Infrastructure.Db;
using DotNet8.EmailVerification.Modules.Account.Infrastructure.Extensions;
using DotNet8.EmailVerification.Utils;
using FluentEmail.Core;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.Modules.Account.Infrastructure.Account
{
    public class UserService : IUserService
    {
        private readonly AccountDbContext _context;
        private readonly IFluentEmail _fluentEmail;
        private readonly string _subject;

        public UserService(AccountDbContext context, IFluentEmail fluentEmail)
        {
            _context = context;
            _fluentEmail = fluentEmail;
            _subject = "Email Verification";
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterUserDto registerUser, CancellationToken cancellationToken)
        {
            Result<UserDto> result;
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                bool isEmailDuplicate = await _context.Tbl_Users.AnyAsync(x => x.Email == registerUser.Email
                    && x.IsActive, cancellationToken: cancellationToken);

                if (isEmailDuplicate)
                {
                    result = Result<UserDto>.Duplicate("Email Duplicate.");
                    goto result;
                }

                var user = registerUser.ToEntity();
                await _context.Tbl_Users.AddAsync(user, cancellationToken);
                string randomCode = GetSixRandomNumbers();

                var setupCode = Extension.ToEntity(user.UserId, randomCode);
                await _context.Tbl_Setups.AddAsync(setupCode, cancellationToken);

                BackgroundJob.Schedule<UserService>(x => x.ExpireCode(setupCode.SetupId, user.UserId, cancellationToken),
                    TimeSpan.FromMinutes(1));

                var response = await _fluentEmail.To(registerUser.Email).Subject(_subject).Body($"Your OTP is: {randomCode}").SendAsync();
                if (!response.Successful)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    result = Result<UserDto>.Failure();
                    goto result;
                }

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                var model = new UserDto()
                {
                    UserId = user.UserId,
                };
                result = Result<UserDto>.Success(model, "Registration Successful.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                result = Result<UserDto>.Failure(ex);
            }

        result:
            return result;
        }

        private string GetSixRandomNumbers()
        {
            Random r = new();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");

            return sixDigitNumber;
        }

        public async Task<Result<SetupDto>> ExpireCode(string setUpId, string userId, CancellationToken cancellationToken)
        {
            Result<SetupDto> result;
            try
            {
                var item = await _context.Tbl_Setups.FindAsync([setUpId], cancellationToken: cancellationToken);
                if (item is null)
                {
                    result = Result<SetupDto>.NotFound();
                    goto result;
                }

                var user = await _context.Tbl_Users.FindAsync([userId], cancellationToken: cancellationToken);
                if (user is null)
                {
                    result = Result<SetupDto>.NotFound();
                    goto result;
                }

                if (!user.IsEmailVerified)
                {
                    _context.Tbl_Users.Remove(user);
                }

                _context.Tbl_Setups.Remove(item);
                await _context.SaveChangesAsync(cancellationToken);

                result = Result<SetupDto>.UpdateSuccess();
            }
            catch (Exception ex)
            {
                result = Result<SetupDto>.Failure(ex);
            }

        result:
            return result;
        }
    }
}
