using System;
using System.Collections.Generic;

namespace BandMate.Domain.Core.Models
{
    public class SetList
    {
        public int SetListID { get; set; }
        public int AccountID { get; set; }
        public int BandID { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        public virtual Account Account { get; set; }
        public virtual Band Band { get; set; }
        public virtual ICollection<Gig> Gigs { get; set; } = new List<Gig>();
        public virtual ICollection<SetListItem> SetListItems { get; set; } = new List<SetListItem>();

    }
}