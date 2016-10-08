using AlpineSkiHouse.Configuration.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Services
{
    public class BlobFileUploadService : IBlobFileUploadService
    {
        private readonly ILogger<AzureStorageSettings> _logger;
        private readonly AzureStorageSettings _storageSettings;

        public BlobFileUploadService(IOptions<AzureStorageSettings> storageSettings, ILogger<AzureStorageSettings> logger)
        {
            _storageSettings = storageSettings.Value;
            _logger = logger;
        }

        public async Task<string> UploadFileFromStream(string containerName, string targetFilename, Stream imageStream)
        {
            var storageAccount = CloudStorageAccount.Parse(_storageSettings.AzureStorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);

            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlockBlobReference(targetFilename);
            await blob.UploadFromStreamAsync(imageStream);
            _logger.LogInformation($"Ski card image uploaded as {targetFilename}");

            return blob?.Uri.ToString();
        }

    }
}
