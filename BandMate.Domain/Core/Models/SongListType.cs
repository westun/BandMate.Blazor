namespace BandMate.Domain.Core.Models
{
    //TODO: update this to just be SongType for jimmy crickets sake
    public class SongListType
    {
        public int SongListTypeID { get; set; }

        public string ListTypeName { get; set; }

        public int BandID { get; set; }

        public int SortOrder { get; set; }

        public virtual Band Band { get; set; }
    }
}