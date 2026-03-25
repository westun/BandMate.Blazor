using System.ComponentModel.DataAnnotations.Schema;

namespace BandMate.Domain.Core.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }

        public virtual Address Address { get; set; }
    }
}
