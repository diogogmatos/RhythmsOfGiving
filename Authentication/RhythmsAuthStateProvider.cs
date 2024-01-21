using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace RhythmsOfGiving.Authentication;

public class RhythmsAuthStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    
    public RhythmsAuthStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
            var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
            if (userSession == null)
                return await Task.FromResult(new AuthenticationState(_anonymous));
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Name, userSession.Name),
                new Claim(ClaimTypes.Role, userSession.Role)
            }, "RhythmsAuth"));
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch (Exception e)
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(UserSession userSession)
    {
        ClaimsPrincipal claimsPrincipal;

        if (userSession != null)
        {
            await _sessionStorage.SetAsync("UserSession", userSession);
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Name, userSession.Name),
                new Claim(ClaimTypes.Role, userSession.Role)
            }));
        }
        else
        {
            await _sessionStorage.DeleteAsync("UserSession");
            claimsPrincipal = _anonymous;
        }
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}