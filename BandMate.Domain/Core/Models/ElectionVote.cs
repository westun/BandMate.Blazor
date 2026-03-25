using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandMate.Domain.Core.Models
{
    public class ElectionVote
    {
        [Column(Order = 1), Key, ForeignKey("Election")] 
        public int ElectionID { get; set; }

        [Column(Order = 2), Key, ForeignKey("Song")]
        public int SongID { get; set; }

        [Column(Order = 3), Key, ForeignKey("Account")]
        public int AccountID { get; set; }
        
        public bool Remove { get; set; }

        public virtual Election Election { get; set; }
        public virtual Song Song { get; set; }
        public virtual Account Account { get; set; }
    }
}