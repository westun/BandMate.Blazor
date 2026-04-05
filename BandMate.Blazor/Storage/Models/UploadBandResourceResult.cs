namespace BandMate.Blazor.Storage.Models
{
    public class UploadBandResourceResult
    {
        public IEnumerable<string> Errors { get; set; }
        public string Url { get; set; }
    }
}
