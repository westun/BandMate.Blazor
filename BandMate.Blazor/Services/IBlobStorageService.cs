namespace BandMate.Blazor.Services
{
    public interface IBlobStorageService
    {
        Task<bool> DeleteBlobAsync(string containerName, string fileName);
        Task<List<string>> ListBlobsAsync(string containerName);
        Task UploadBlobAsync(string containerName, string fileName, Stream content, string contentType);
    }
}