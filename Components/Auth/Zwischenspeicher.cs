using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using LernCockpit.Data;

namespace LernCockpit.Components.Auth;

public class Zwischenspeicher : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    // Hier habe ich die Factory hinzugefügt, um Zugriff auf die Datenbank zu bekommen
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
    
    private ClaimsPrincipal _anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

    // Im Konstruktor habe ich die DB-Factory injiziert
    public Zwischenspeicher(ProtectedSessionStorage sessionStorage, IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _sessionStorage = sessionStorage;
        _dbFactory = dbFactory;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var result = await _sessionStorage.GetAsync<string>("UserSession");

            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                var email = result.Value;
                
                // Hier rufe ich die Hilfsmethode auf, um die Rollen aus der DB zu laden
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

    public async Task UpdateAuthenticationState(string? email)
    {
        ClaimsPrincipal user;

        if (!string.IsNullOrEmpty(email))
        {
            await _sessionStorage.SetAsync("UserSession", email);
            
            // Auch beim Login lade ich die Claims frisch aus der Datenbank
            var claims = await GetClaimsFromDb(email);
            
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
        }
        else
        {
            await _sessionStorage.DeleteAsync("UserSession");
            user = _anonymousUser;
        }
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    // In dieser Methode wandle ich den Boolean aus der Datenbank in eine Rolle um
    private async Task<List<Claim>> GetClaimsFromDb(string email)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Email, email),
        };

        try 
        {
            // Hier öffne ich kurz eine Datenbankverbindung
            using var db = _dbFactory.CreateDbContext();
            var dbUser = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            
            if (dbUser != null)
            {
                // Hier prüfe ich, ob der User ein Ausbilder ist (Boolean Feld)
                if (dbUser.IsAusbilder) 
                {
                    // Wenn ja, füge ich die Rolle "Ausbilder" hinzu
                    claims.Add(new Claim(ClaimTypes.Role, "Ausbilder"));
                }
            }
        }
        catch
        {
            // Falls DB down ist, logge ich ihn trotzdem als normalen User ein
        }

        return claims;
    }
}