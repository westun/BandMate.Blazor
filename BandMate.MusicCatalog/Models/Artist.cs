using System.Collections.Generic;

namespace BandMate.MusicCatalog.Models
{
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}
