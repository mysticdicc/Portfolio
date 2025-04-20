//using Portfolio.Client.Pages;
using Portfolio.Client;
using Portfolio.Components;
using Radzen;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddControllers();
builder.Services.AddRadzenComponents();

builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "RadzenBlazorApp1Theme";
    options.Duration = TimeSpan.FromDays(365);
});

builder.Services.AddScoped(sp =>
{
    NavigationManager navigation = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri("https://portfolio.danknet.uk/") };
});
builder.Services.AddHttpClient();

builder.Services.AddTransient<PortfolioClassLibrary.DevProjectAPI>();
builder.Services.AddTransient<PortfolioClassLibrary.ItProjectAPI>();
builder.Services.AddTransient<PortfolioClassLibrary.BlogPostAPI>();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("EntraStuff"));
builder.Services.AddAuthorization();

builder.Services.AddDbContextFactory<PortfolioDatabase>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseStaticFiles();

app.MapControllers();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Portfolio.Client._Imports).Assembly);
app.MapGroup("/authentication").MapLoginAndLogout();

app.Run();
