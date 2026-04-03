using Microsoft.AspNetCore.Components.Authorization;

namespace BandMate.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        public UserService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<int> GetCurrentAccountIDAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated ?? false)
            {
                var claim = user.FindFirst(c => c.Type == "AccountID");
                if (claim != null && int.TryParse(claim.Value, out var accountID))
                {
                    return accountID;
                }
            }

            return 0;
        }
    }
}
