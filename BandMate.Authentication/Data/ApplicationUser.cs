using Microsoft.AspNetCore.Identity;

namespace BandMate.Authentication.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int AccountID { get; set; }
    }

}
