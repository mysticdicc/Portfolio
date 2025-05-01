using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

using PortfolioClassLibrary.Classes.Blog;
using PortfolioClassLibrary.Classes.DevProjects;
using PortfolioClassLibrary.Classes.Images;
using PortfolioClassLibrary.Classes.ItProjects;

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

builder.Services.AddTransient<DevProjectAPI>();
builder.Services.AddTransient<ItProjectAPI>();
builder.Services.AddTransient<BlogPostAPI>();
builder.Services.AddTransient<ImageAPI>();

await builder.Build().RunAsync();
