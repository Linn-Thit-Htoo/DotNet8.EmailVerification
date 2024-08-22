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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser, CancellationToken cancellationToken)
    {
        var result = await _userService.RegisterAsync(registerUser, cancellationToken);
        return Content(result);
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequestDto confirmEmailRequest, CancellationToken cancellationToken)
    {
        var result = await _userService.ConfirmEmailAsync(confirmEmailRequest, cancellationToken);
        return Content(result);
    }
}