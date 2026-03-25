namespace BandMate.Domain.Core.Models
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int SongID { get; set; }
        public int AccountID { get; set; }
        public double Rated { get; set; }
        public double Priority { get; set; }

        public virtual Song Song { get; set; }
        public virtual Account Account { get; set; }
    }
}