using System.Collections.Generic;

namespace BandMate.Domain.Core.Models
{
    public class Band
    {
        public int BandID { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<BandAccount> BandAccounts { get; set; }
        public virtual ICollection<SetList> SetLists { get; set; }
        public virtual ICollection<Song> Songs { set; get; }
        public virtual ICollection<BandName> BandNames { get; set; }
    }
}