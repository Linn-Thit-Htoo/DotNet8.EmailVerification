﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.DTOs.Features.Account
{
    public class ConfirmEmailRequestDto
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
