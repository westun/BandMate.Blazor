using BandMate.Shared.Security;
using BandMate.Shared.Storage.Models;
using Microsoft.Extensions.Logging;

namespace BandMate.Shared.Storage
{
    public class BandResourceManager : IBandResourceManager
    {
        private const string PolicyId = "BandMateReadOnlyPolicy";
        private readonly IBlobStorageService blobService;
        private readonly ILogger<IBandResourceManager> logger;
        private readonly IFileUploadValidator fileUploadValidator;

        public BandResourceManager(
            IBlobStorageService blobService, 
            ILogger<IBandResourceManager> logger,
            IFileUploadValidator fileUploadValidator)
        {
            this.blobService = blobService;
            this.logger = logger;
            this.fileUploadValidator = fileUploadValidator;
        }

        public async Task<UploadBandResourceResult> UploadResourceAsync(int bandId, string fileName, Stream content, string contentType)
        {
            return await this.UploadResourceAsync(bandId, fileName, content, contentType, null);
        }

        public async Task<UploadBandResourceResult> UploadResourceAsync(int bandId, string fileName, Stream content, string contentType, Progress<long> progressHandler)
        {
            if (!this.fileUploadValidator.IsExtensionAllowed(fileName))
            {
                return this.CreateBandResourceResult("Invalid file extension");
            }

            try
            {
                var containerName = GetBlobStorageSafeBandName(bandId);
                var blobUploadResult = await this.blobService.UploadBlobAsync(containerName, fileName, content, contentType, progressHandler);
                return new UploadBandResourceResult { Url = blobUploadResult.Url };

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Failed to upload resource for Band {bandId} | file name: {fileName}", bandId);
                throw;
            }
}

        public async Task<List<string>> GetFilesAsync(int bandId)
        {
            var containerName = this.GetBlobStorageSafeBandName(bandId);
            return await this.blobService.ListBlobsAsync(containerName);
        }

        public async Task DeleteResourceAsync(int bandId, string fileName)
        {
            this.logger.LogInformation("Deleting file {FileName}", fileName);
            var containerName = this.GetBlobStorageSafeBandName(bandId);
            await this.blobService.DeleteBlobAsync(containerName, fileName);
        }

        public async Task<string> GetSasUrlAsync(int bandId, string fileName)
        {
            var containerName = this.GetBlobStorageSafeBandName(bandId);
            return await this.blobService.GetSasUrlWithPolicyAsync(containerName, fileName, PolicyId);
        }

        private string GetBlobStorageSafeBandName(int bandID)
        {
            var containerName = bandID.ToString().PadLeft(5, '0');

            return containerName.Trim().ToLower().Replace(" ", "");
        }

        private UploadBandResourceResult CreateBandResourceResult(params IEnumerable<string> errors)
        {
            return new UploadBandResourceResult { Errors = errors };
        }
    }
}
