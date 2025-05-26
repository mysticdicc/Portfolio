using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

using PortfolioClassLibrary.Classes.Blog;
using PortfolioClassLibrary.Classes.DevProjects;
using PortfolioClassLibrary.Classes.Images;
using PortfolioClassLibrary.Classes.ItProjects;
using Portfolio.Client.Services;

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

var baseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseAddress")!);

builder.Services.AddScoped(sp =>
{
    NavigationManager navigation = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = baseAddress };
});

SharedServices.Register(builder.Services, baseAddress);

await builder.Build().RunAsync();
