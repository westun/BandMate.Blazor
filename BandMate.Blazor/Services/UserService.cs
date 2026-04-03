using Microsoft.AspNetCore.Components.Authorization;

namespace BandMate.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider authStateProvider;
        private int? cachedAccountId;

        public UserService(AuthenticationStateProvider authStateProvider)
        {
            this.authStateProvider = authStateProvider;
        }

        public async Task<int> GetCurrentAccountIDAsync()
        {
            if (this.cachedAccountId.HasValue)
            {
                return this.cachedAccountId.Value;
            }

            var authState = await this.authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated ?? false)
            {
                var claim = user.FindFirst(c => c.Type == "AccountID");
                if (claim != null && int.TryParse(claim.Value, out var accountID))
                {
                    this.cachedAccountId = accountID;
                    return accountID;
                }
            }

            return 0;
        }
    }
}
