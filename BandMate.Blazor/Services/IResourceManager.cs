namespace BandMate.Blazor.Services
{
    public interface IBandResourceManager
    {
        Task DeleteResourceAsync(int bandId, string fileName);
        Task<List<string>> GetFilesAsync(int bandId);
        Task<bool> UploadResourceAsync(int bandId, string fileName, Stream content, string contentType);
    }
}