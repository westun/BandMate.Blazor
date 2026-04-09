using BandMate.Shared.Storage.Models;

namespace BandMate.Shared.Storage
{
    public interface IBlobStorageService
    {
        Task AddContainerPolicyIfNotExistsAsync(string containerName, string policyID);
        Task<bool> DeleteBlobAsync(string containerName, string fileName);
        Task<string> GetSasUrlWithPolicyAsync(string containerName, string fileName, string policyID);
        Task<List<string>> ListBlobsAsync(string containerName);
        Task<UploadBlobResult> UploadBlobAsync(string containerName, string fileName, Stream content, string contentType);
        Task<UploadBlobResult> UploadBlobAsync(string containerName, string fileName, Stream content, string contentType, Progress<long> progressHandler);
    }
}