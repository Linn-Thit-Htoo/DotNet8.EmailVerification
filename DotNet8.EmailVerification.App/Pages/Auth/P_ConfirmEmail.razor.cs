namespace DotNet8.EmailVerification.App.Pages.Auth;

public partial class P_ConfirmEmail
{
    public ConfirmEmailRequestDto RequestModel { get; set; } = new();

    public async Task Confirm()
    {
        string? userId = await LocalStorage.GetItemAsStringAsync("UserId");
        if (userId is null)
        {
            Snackbar.Add("User Not Found.", Severity.Error);
            return;
        }

        RequestModel.UserId = userId;

        var result = await HttpClientService.ExecuteAsync<Result<UserDto>>(
            "/api/account/confirm-email",
            EnumHttpMethod.POST,
            RequestModel
        );
        if (result.IsSuccess)
        {
            Snackbar.Add(result.Message, Severity.Success);
            Navigation.NavigateTo("/");
            return;
        }

        Snackbar.Add(result.Message, Severity.Error);
    }
}
