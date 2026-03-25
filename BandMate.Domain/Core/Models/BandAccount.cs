using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandMate.Domain.Core.Models
{
    public class BandAccount
    {
        [Column(Order = 1), Key, ForeignKey("Band")]
        public int BandID { get; set; }
        [Column(Order = 2), Key, ForeignKey("Account")]
        public int AccountID { get; set; }
        public bool Default { get; set; }
        //public bool IsOwner { get; set; }

        public virtual Band Band { get; set; }
        public virtual Account Account { get; set; }
    }
}
