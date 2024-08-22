namespace DotNet8.EmailVerification.Modules.Account.Domain.Account;

public interface IUserService
{
    Task<Result<UserDto>> RegisterAsync(RegisterUserDto registerUser, CancellationToken cancellationToken);
    Task<Result<UserDto>> ConfirmEmailAsync(ConfirmEmailRequestDto confirmEmailRequest, CancellationToken cancellationToken);
}