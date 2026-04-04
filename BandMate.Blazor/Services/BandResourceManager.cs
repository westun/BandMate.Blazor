namespace BandMate.Blazor.Services
{
    public class BandResourceManager : IBandResourceManager
    {
        private readonly IBlobStorageService _blobService;
        private readonly ILogger<IBandResourceManager> _logger;

        public BandResourceManager(IBlobStorageService blobService, ILogger<IBandResourceManager> logger)
        {
            _blobService = blobService;
            _logger = logger;
        }

        public async Task<bool> UploadResourceAsync(int bandId, string fileName, Stream content, string contentType)
        {
            try
            {
                var containerName = GetBlobStorageSafeBandName(bandId);
                await _blobService.UploadBlobAsync(containerName, fileName, content, contentType);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload resource for Band {BandId}", bandId);
                return false;
            }
        }

        public async Task<List<string>> GetFilesAsync(int bandId)
        {
            var containerName = GetBlobStorageSafeBandName(bandId);
            return await _blobService.ListBlobsAsync(containerName);
        }

        public async Task DeleteResourceAsync(int bandId, string fileName)
        {
            _logger.LogInformation("Deleting file {FileName}", fileName);
            var containerName = GetBlobStorageSafeBandName(bandId);
            await _blobService.DeleteBlobAsync(containerName, fileName);
        }

        private string GetBlobStorageSafeBandName(int bandID)
        {
            var containerName = bandID.ToString().PadLeft(5, '0');

            return containerName.Trim().ToLower().Replace(" ", "");
        }
    }


}
