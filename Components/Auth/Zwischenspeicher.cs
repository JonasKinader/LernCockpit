using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using LernCockpit.Data;

namespace LernCockpit.Components.Auth;

public class Zwischenspeicher : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _localStorage;
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
    
    private ClaimsPrincipal _anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

    public Zwischenspeicher(
        ProtectedLocalStorage localStorage, 
        ProtectedSessionStorage sessionStorage, 
        IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _localStorage = localStorage;
        _sessionStorage = sessionStorage;
        _dbFactory = dbFactory;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // Erst LocalStorage prüfen (angemeldet bleiben)
            var result = await _localStorage.GetAsync<string>("UserSession");
            
            // Falls nicht gefunden, SessionStorage prüfen
            if (!result.Success || string.IsNullOrEmpty(result.Value))
            {
                result = await _sessionStorage.GetAsync<string>("UserSession");
            }

            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                var email = result.Value;
                var claims = await GetClaimsFromDb(email);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
                return new AuthenticationState(user);
            }
        }
        catch
        {
            // Fehler ignorieren
        }
        
        return new AuthenticationState(_anonymousUser);
    }

    // NEU: Parameter für "angemeldet bleiben"
    public async Task UpdateAuthenticationState(string? email, bool angemeldetBleiben = false)
    {
        ClaimsPrincipal user;

        if (!string.IsNullOrEmpty(email))
        {
            if (angemeldetBleiben)
            {
                // Dauerhaft speichern
                await _localStorage.SetAsync("UserSession", email);
            }
            else
            {
                // Nur für diese Sitzung
                await _sessionStorage.SetAsync("UserSession", email);
            }
            
            var claims = await GetClaimsFromDb(email);
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
        }
        else
        {
            // Logout - beide Storage-Arten löschen
            await _localStorage.DeleteAsync("UserSession");
            await _sessionStorage.DeleteAsync("UserSession");
            user = _anonymousUser;
        }
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private async Task<List<Claim>> GetClaimsFromDb(string email)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Email, email),
        };

        try 
        {
            using var db = _dbFactory.CreateDbContext();
            var dbUser = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            
            if (dbUser != null)
            {
                if (dbUser.IsAusbilder) 
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Ausbilder"));
                }
            }
        }
        catch
        {
            // Falls DB down ist, trotzdem als normalen User einloggen
        }

        return claims;
    }
}