﻿namespace DotNet8.EmailVerification.DTOs.Features.Account;

public class UserDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public bool IsEmailVerified { get; set; }
}
