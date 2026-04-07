using Azure;
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
            return await this.UploadBlobAsync(containerName, fileName, content, contentType, null);
        }

        public async Task<UploadBlobResult> UploadBlobAsync(string containerName, string fileName, Stream content, string contentType, Progress<long> progressHandler)
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName.ToLower());
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);
            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
            };

            if (progressHandler is not null)
            {
                options.ProgressHandler = progressHandler;
                //this will allow progress bars to show updates more often for smaller, and bigger, files
                //until able to test in a hosted environment, this is a mute point
                //options.TransferOptions = new StorageTransferOptions
                //{
                //    // Smaller chunks (e.g., 512KB) force the progress handler to fire more often
                //    InitialTransferSize = 512 * 1024,
                //    MaximumTransferSize = 512 * 1024
                //};
            }

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
                //do not set expiration here if it is set in the policy
                Identifier = policyID,
                BlobContainerName = containerName,
                BlobName = fileName,
                Resource = "b",
            };

            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }

        public async Task AddContainerPolicyIfNotExistsAsync(string containerName, string policyId)
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);

            //these variables are only created to allow try catch to work
            //would simply call create if not exists everytime, but this method is often called in a loop
            var emptyPolicy = new BlobContainerAccessPolicy();
            var accessPolicy = Response.FromValue(emptyPolicy, null!);
            try
            {
                accessPolicy = await containerClient.GetAccessPolicyAsync();
            }
            catch(RequestFailedException ex)
            {
                containerClient.CreateIfNotExists();
                accessPolicy = await containerClient.GetAccessPolicyAsync();
            }

            // 1. Fetch current policies first so we don't overwrite them
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
