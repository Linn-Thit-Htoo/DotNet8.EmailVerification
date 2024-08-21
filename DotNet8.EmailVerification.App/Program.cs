using Blazored.LocalStorage;
using DotNet8.EmailVerification.App;
using DotNet8.EmailVerification.App.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7139") });

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddScoped<HttpClientService>();

await builder.Build().RunAsync();
