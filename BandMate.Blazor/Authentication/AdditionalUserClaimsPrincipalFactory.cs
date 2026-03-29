using BandMate.Blazor.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BandMate.Blazor.Authentication
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor) // Base now only needs 2 arguments
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            // 1. Get the base identity
            var identity = await base.GenerateClaimsAsync(user);

            // 2. Add your custom properties as claims
            // "CustomTag" is the key you will use to retrieve it later
            if (user.AccountID > 0)
            {
                identity.AddClaim(new Claim("AccountID", user.AccountID.ToString()));
            }

            return identity;
        }
    }

}
