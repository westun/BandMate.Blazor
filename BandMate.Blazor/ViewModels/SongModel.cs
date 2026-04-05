using BandMate.Blazor.DTO;

namespace BandMate.Blazor.ViewModels
{
    public class SongModel
    {
        public bool UploadHasError { get; set; }
        public SongDTO Song { get; set; }
    }
}
