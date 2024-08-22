namespace DotNet8.EmailVerification.DTOs.Features.Account;

public class ConfirmEmailRequestDto
{
    public string UserId { get; set; }
    public string Code { get; set; }
}
