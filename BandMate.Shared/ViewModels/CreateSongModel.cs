using System.ComponentModel.DataAnnotations;

namespace BandMate.Shared.ViewModels
{
    public class CreateSongModel
    {
        [Required(ErrorMessage = "Song artist is required.")]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Song title is required.")]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Title { get; set; }
    }
}
