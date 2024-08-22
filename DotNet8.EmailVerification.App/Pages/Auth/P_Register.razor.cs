namespace DotNet8.EmailVerification.App.Pages.Auth;

public partial class P_Register
{
    public RegisterUserDto RequestModel { get; set; } = new();

    public async Task Register()
    {
        var result = await HttpClientService.ExecuteAsync<Result<UserDto>>(
            "/api/account/register",
            EnumHttpMethod.POST,
            RequestModel
        );
        if (result.IsSuccess)
        {
            await LocalStorage.SetItemAsStringAsync("UserId", result.Data.UserId);
            Snackbar.Add(result.Message, Severity.Success);
            Navigation.NavigateTo("/confirm-email");
        }
        else
        {
            Snackbar.Add(result.Message, Severity.Error);
        }
    }
}
