using System.ComponentModel.DataAnnotations;

namespace BandMate.Domain.Core.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MaxLength(100)]
        //[Index("AccountEmailIndex", IsUnique = true)]
        public string Email { get; set; }

        [MaxLength(30)]
        //[Index("AccountUsernameIndex", IsUnique = true)]
        public string Username { get; set; }

        public string B2CObjectIdentifier { get; set; }

        public virtual ICollection<BandAccount> BandAccounts { get; set; }
        public virtual ICollection<AccountCredential> AccountCredentials { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<ElectionVote> ElectionVotes { get; set; }
        public virtual ICollection<SongAccount> SongAccounts { get; set; }
    }
}
