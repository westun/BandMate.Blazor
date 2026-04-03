using Microsoft.AspNetCore.Components.Authorization;

namespace BandMate.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        private int? _cachedAccountId;
        private bool? _isAuthenticated;

        public UserService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            if (_isAuthenticated.HasValue) return _isAuthenticated.Value;

            await InitializeAsync();
            return _isAuthenticated ?? false;
        }

        public async Task<int> GetCurrentAccountIDAsync()
        {
            if (_cachedAccountId.HasValue) return _cachedAccountId.Value;

            await InitializeAsync();
            return _cachedAccountId ?? 0;
        }

        private async Task InitializeAsync()
        {
            // Only run the expensive auth check once
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            _isAuthenticated = user.Identity?.IsAuthenticated ?? false;

            if (_isAuthenticated.Value)
            {
                var claim = user.FindFirst(c => c.Type == "AccountID");
                if (claim != null && int.TryParse(claim.Value, out var accountID))
                {
                    _cachedAccountId = accountID;
                }
            }
            else
            {
                _cachedAccountId = 0;
            }
        }
    }

}
