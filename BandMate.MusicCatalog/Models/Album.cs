using System;
using System.Collections.Generic;

namespace BandMate.MusicCatalog.Models
{
    public class Album
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AlbumImgCoverUrl { get; set; }
        public string Release_Date { get; set; }
        public IEnumerable<Song> Tracks { get; set; }
    }
}
