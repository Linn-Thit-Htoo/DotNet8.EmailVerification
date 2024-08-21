using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.Modules.Account.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.Modules.Account.Infrastructure.Extensions
{
    public static class Extension
    {
        public static Tbl_User ToEntity(this RegisterUserDto registerUser)
        {
            return new Tbl_User
            {
                UserId = Ulid.NewUlid().ToString(),
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                Password = registerUser.Password,
                IsEmailVerified = false,
                IsActive = true
            };
        }

        public static Tbl_Setup ToEntity(string userId, string setupCode)
        {
            return new Tbl_Setup
            {
                SetupId = Ulid.NewUlid().ToString(),
                UserId = userId,
                SetupCode = setupCode,
                CreatedDate = DateTime.Now,
            };
        }
    }
}
