using System;

namespace BandMate.Domain.Core.Models
{
    public class Gig
    {
        public int GigID { get; set; }
        public int VenueID { get; set; }
        public int BandID { get; set; }
        public int? SetListID { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        
        public virtual Venue Venue { get; set; }
        public virtual Band Band { get; set; }
        public virtual SetList SetList { get; set; }
    }
}
