namespace BandMate.Blazor.Storage.Models
{
    public class UploadBlobResult
    {
        public IEnumerable<string> Errors { get; set; }
        public string Url { get; set; }
    }
}
