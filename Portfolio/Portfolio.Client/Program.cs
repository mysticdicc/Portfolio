using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddRadzenComponents();

builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "RadzenBlazorApp1Theme";
    options.Duration = TimeSpan.FromDays(365);
});

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddScoped(sp =>
{
    NavigationManager navigation = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri("https://portfolio.danknet.uk") };
});

builder.Services.AddTransient<PortfolioClassLibrary.DevProjectAPI>();
builder.Services.AddTransient<PortfolioClassLibrary.ItProjectAPI>();



await builder.Build().RunAsync();
