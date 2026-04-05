using BandMate.Blazor.Enum;
using Microsoft.AspNetCore.Components.Forms;

namespace BandMate.Blazor.ViewModels
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
