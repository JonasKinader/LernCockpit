using MudBlazor.Services;
using LernCockpit.Components;
using LernCockpit.Components.Auth;
using LernCockpit.Components.Services;
using Microsoft.EntityFrameworkCore;
using LernCockpit.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor;


var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices(
    config => config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight);

// --- SPEICHER DIENSTE ---
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<ProtectedLocalStorage>();
// ------------------------

builder.Services.AddScoped<AuthenticationStateProvider, Zwischenspeicher>();
builder.Services.AddScoped<ILearningService, LearningService>();
builder.Services.AddCascadingAuthenticationState();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
builder.Services.AddAuthorization();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Erhöhe Request-Limits für Session Storage
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestLineSize = 32768;
    options.Limits.MaxRequestHeadersTotalSize = 131072;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();


app.UseAuthentication();
app.UseAuthorization();


app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();