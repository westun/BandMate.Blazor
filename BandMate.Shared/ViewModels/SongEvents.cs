using BandMate.Shared.Enum;
using Microsoft.AspNetCore.Components.Forms;

namespace BandMate.Shared.ViewModels
{
    public class SongEvents
    {
        public record SongFileChangedEventArgs(
            InputFileChangeEventArgs FileArgs,
            SongListGridItemModel SongModel,
            FileUploadType FileUploadType
        );
    }
}
