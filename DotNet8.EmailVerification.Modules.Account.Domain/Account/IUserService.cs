﻿using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.Modules.Account.Domain.Account
{
    public interface IUserService
    {
        Task<Result<UserDto>> Register(RegisterUserDto registerUser, CancellationToken cancellationToken);
    }
}