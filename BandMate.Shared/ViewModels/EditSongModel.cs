using System.ComponentModel.DataAnnotations;

namespace BandMate.Shared.ViewModels
{
    public class EditSongModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select a song type.")]
        public int SongListTypeID { get; set; } = 0;

        [Required(ErrorMessage = "Song artist is required.")]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Song title is required.")]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Title { get; set; }

        public int? Bpm { get; set; }
        public string? KeySignature { get; set; }
        public string? YoutubeURL { get; set; }
        public string? SheetMusicURL { get; set; }
        public string? LyricURL { get; set; }
        public string? Notes { get; set; }
    }
}
