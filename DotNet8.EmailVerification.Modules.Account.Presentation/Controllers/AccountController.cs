namespace DotNet8.EmailVerification.Modules.Account.Presentation.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : BaseController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    #region Register

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserDto registerUser,
        CancellationToken cancellationToken
    )
    {
        var result = await _userService.RegisterAsync(registerUser, cancellationToken);
        return Content(result);
    }

    #endregion

    #region Confirm Email

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(
        [FromBody] ConfirmEmailRequestDto confirmEmailRequest,
        CancellationToken cancellationToken
    )
    {
        var result = await _userService.ConfirmEmailAsync(confirmEmailRequest, cancellationToken);
        return Content(result);
    }

    #endregion
}
