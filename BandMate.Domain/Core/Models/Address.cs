using System.Collections.Generic;

namespace BandMate.Domain.Core.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }
    }
}
