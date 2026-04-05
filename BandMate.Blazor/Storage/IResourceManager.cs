using BandMate.Blazor.Storage.Models;

namespace BandMate.Blazor.Storage
{
    public interface IBandResourceManager
    {
        Task DeleteResourceAsync(int bandId, string fileName);
        Task<List<string>> GetFilesAsync(int bandId);
        Task<string> GetSasUrlAsync(int bandId, string fileName);
        Task<UploadBandResourceResult> UploadResourceAsync(int bandId, string fileName, Stream content, string contentType);
    }
}