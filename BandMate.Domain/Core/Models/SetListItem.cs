namespace BandMate.Domain.Core.Models
{
    public class SetListItem
    {
        public int SetListItemID { get; set; }
        public int SongID { get; set; }
        public int SetListID { get; set; }
        public int Order { get; set; }
        public string? Note { get; set; }

        public virtual Song Song { get; set; }
        public virtual SetList SetList { get; set; }        
    }
}
