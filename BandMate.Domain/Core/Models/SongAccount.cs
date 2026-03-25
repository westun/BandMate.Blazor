using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandMate.Domain.Core.Models
{
    public class SongAccount
    {
        [Column(Order = 1), Key, ForeignKey("Song")]
        public int SongID { get; set; }

        [Column(Order = 2), Key, ForeignKey("Account")]
        public int AccountID { get; set; }

        public string Notes { get; set; }

        public virtual Song Song { get; set; }
        public virtual Account Account { get; set; }

    }
}
