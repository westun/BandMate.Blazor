using System.ComponentModel.DataAnnotations;

namespace BandMate.Blazor.ViewModels
{
    public class SongTypeModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select a song type.")]
        public int SongTypeID { get; set; } = 0;
    }
}
