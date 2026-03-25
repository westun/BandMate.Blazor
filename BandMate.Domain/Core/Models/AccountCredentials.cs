using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BandMate.Domain.Core.Models
{
    public class AccountCredential
    {
        [Key]
        public int AccountCredentialID { get; set; }

        [ForeignKey("Account")]
        public int AccountID { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }
        public bool Active { get; set; }
        public string Version { get; set; }
        
        [JsonIgnore]
        public Account Account { get; set; }
    }
}
