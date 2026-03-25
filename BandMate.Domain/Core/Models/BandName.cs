using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandMate.Domain.Core.Models
{
    public class BandName
    {
        [Column("BandNameID")]
        public int ID { get; set; }

        [ForeignKey("Band")]
        public int BandID { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual Band Band { get; set; }
    }
}
