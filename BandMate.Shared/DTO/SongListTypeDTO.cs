using System.ComponentModel.DataAnnotations;

namespace BandMate.Shared.DTO
{
    public class SongListTypeDTO
    {
        public int SongListTypeID { get; set; }

        [Required(ErrorMessage = "Song Type Name is required.")]
        public string ListTypeName { get; set; }
        public int BandID { get; set; }
        public int SortOrder { get; set; }
    }
}
