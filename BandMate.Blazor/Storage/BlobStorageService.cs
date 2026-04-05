using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using BandMate.Blazor.Storage.Models;

namespace BandMate.Blazor.Storage
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient blobServiceClient;

        public BlobStorageService(IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:StorageConnection"];
            this.blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<UploadBlobResult> UploadBlobAsync(string containerName, string fileName, Stream content, string contentType)
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName.ToLower());
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);
            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
            };

            await blobClient.UploadAsync(content, options);

            return new UploadBlobResult { Url = blobClient.Uri.AbsoluteUri };
        }

        public async Task<List<string>> ListBlobsAsync(string containerName)
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName.ToLower());
            var names = new List<string>();

            if (await containerClient.ExistsAsync())
            {
                await foreach (var blobItem in containerClient.GetBlobsAsync())
                {
                    names.Add(blobItem.Name);
                }
            }
            return names;
        }

        public async Task<bool> DeleteBlobAsync(string containerName, string fileName)
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName.ToLower());
            var blobClient = containerClient.GetBlobClient(fileName);
            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> GetSasUrlWithPolicyAsync(string containerName, string fileName, string policyID)
        {
            await this.AddContainerPolicyIfNotExistsAsync(containerName, policyID);

            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var sasBuilder = new BlobSasBuilder
            {
                Identifier = policyID, // Reference the Stored Access Policy ID
                BlobContainerName = containerName,
                BlobName = fileName,
                Resource = "b",
                ExpiresOn = DateTime.UtcNow.AddMinutes(30),
            };

            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }

        public async Task AddContainerPolicyIfNotExistsAsync(string containerName, string policyId)
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);

            // 1. Fetch current policies first so we don't overwrite them
            var accessPolicy = await containerClient.GetAccessPolicyAsync();
            var existingIdentifiers = accessPolicy.Value.SignedIdentifiers.ToList();

            if (existingIdentifiers.Any(x => x.Id == policyId))
            {
                return;
            }

            // 2. Check if we've reached the Azure limit (Max 5)
            if (existingIdentifiers.Count >= 5 && !existingIdentifiers.Any(x => x.Id == policyId))
            {
                throw new Exception("Cannot add more than 5 policies to a container.");
            }

            // 3. Create or update the specific policy
            var newIdentifier = new BlobSignedIdentifier
            {
                Id = policyId,
                AccessPolicy = new BlobAccessPolicy
                {
                    StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
                    ExpiresOn = DateTimeOffset.UtcNow.AddYears(1),
                    Permissions = "r"
                }
            };

            existingIdentifiers.Add(newIdentifier);

            // 4. Upload the FULL list back to Azure
            await containerClient.SetAccessPolicyAsync(permissions: existingIdentifiers);
        }

    }
}
