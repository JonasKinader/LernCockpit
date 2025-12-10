using MudBlazor.Services;
using LernCockpit.Components;
using LernCockpit.Components.Auth;
using Microsoft.EntityFrameworkCore;
using LernCockpit.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// added den sessionsicheren speicher
builder.Services.AddScoped<ProtectedSessionStorage>();

// ersetzt den standart auth-provider durch meinen eigenen zwischenspeicher
builder.Services.AddScoped<AuthenticationStateProvider, Zwischenspeicher>();

// gibt den loginstatus an alle komponenten weiter
builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure Entity Framework and MySQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
