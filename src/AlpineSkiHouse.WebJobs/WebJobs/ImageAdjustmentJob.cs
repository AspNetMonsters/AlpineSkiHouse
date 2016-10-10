using ImageProcessorCore;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.WebJobs.WebJobs
{
    public class ImageAdjustments
    {
        public static void ResizeAndGrayscaleImage(
            [QueueTrigger("skicard-imageprocessing")] string message,
            [Blob("cardimages/{queueTrigger}", FileAccess.Read)] Stream imageStream,
            [Blob("processed/{queueTrigger}", FileAccess.Write)] Stream resizedImageStream)
        {
            var image = new Image(imageStream);

            var resizedImage = image
                .Resize(100, 0)
                .Grayscale(GrayscaleMode.Bt709);

            resizedImage.SaveAsJpeg(resizedImageStream);
        }
    }
}
