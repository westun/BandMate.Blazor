using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandMate.Domain.Core.Models
{
    /// <summary>
    /// this entity exists with no payload to turn off cascade delete
    /// </summary>
    public class ElectionSong
    {
        [Column(Order = 1), Key, ForeignKey("Election")]
        public int ElectionID { get; set; }

        [Column(Order = 2), Key, ForeignKey("Song")]
        public int SongID { get; set; }

        public virtual Election Election { get; set; }
        public virtual Song Song { get; set; }
    }
}
