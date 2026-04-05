using BandMate.Blazor.Storage.Models;

namespace BandMate.Blazor.Storage
{
    public interface IBlobStorageService
    {
        Task AddContainerPolicyIfNotExistsAsync(string containerName, string policyID);
        Task<bool> DeleteBlobAsync(string containerName, string fileName);
        Task<string> GetSasUrlWithPolicyAsync(string containerName, string fileName, string policyID);
        Task<List<string>> ListBlobsAsync(string containerName);
        Task<UploadBlobResult> UploadBlobAsync(string containerName, string fileName, Stream content, string contentType);
    }
}