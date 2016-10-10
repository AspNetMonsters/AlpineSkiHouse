using AlpineSkiHouse.Configuration.Models;
using AlpineSkiHouse.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Handlers
{
    public class QueueResizeOnSkiCardImageUploadedHandler : IAsyncNotificationHandler<SkiCardImageUploaded>
    {
        private readonly ILogger<QueueResizeOnSkiCardImageUploadedHandler> _logger;
        private readonly AzureStorageSettings _storageSettings;

        public QueueResizeOnSkiCardImageUploadedHandler(IOptions<AzureStorageSettings> storageSettings, ILogger<QueueResizeOnSkiCardImageUploadedHandler> logger)
        {
            _logger = logger;
            _storageSettings = storageSettings.Value;
        }

        public async Task Handle(SkiCardImageUploaded notification)
        {
            // prepare the queue client
            var storageAccount = CloudStorageAccount.Parse(_storageSettings.AzureStorageConnectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var imageQueue = queueClient.GetQueueReference("skicard-imageprocessing");
            await imageQueue.CreateIfNotExistsAsync();

            // prepare and send the message to the queue
            var message = new CloudQueueMessage(notification.FileName);
            await imageQueue.AddMessageAsync(message);

            _logger.LogInformation($"Published image uploaded message for {notification.FileName} to queue.");
        }
    }
}
