using BandMate.Blazor.DTO;

namespace BandMate.Blazor.ViewModels
{
    public class SongListGridItemModel
    {
        public string UploadErrorMessage { get; set; }
        public string UploadSuccessMessage { get; set; }
        public SongDTO Song { get; set; } = new();
    }
}
