using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.Modules.Account.Domain.Account;
using DotNet8.EmailVerification.Modules.Account.Infrastructure.Db;
using DotNet8.EmailVerification.Utils;
using Microsoft.EntityFrameworkCore;
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

        public UserService(AccountDbContext context)
        {
            _context = context;
        }

        public async Task<Result<UserDto>> Register(RegisterUserDto registerUser, CancellationToken cancellationToken)
        {
            Result<UserDto> result;
            try
            {
                bool isEmailDuplicate = await _context.Tbl_Users.AnyAsync(x => x.Email == registerUser.Email
                    && x.IsEmailVerified && x.IsActive, cancellationToken: cancellationToken);

                if (isEmailDuplicate)
                {
                    result = Result<UserDto>.Duplicate("Email Duplicate.");
                    goto result;
                }


            }
            catch (Exception ex)
            {
                result = Result<UserDto>.Failure(ex);
            }

        result:
            return result;
        }
    }
}
