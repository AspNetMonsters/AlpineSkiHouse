using ImageProcessor;
using Microsoft.Azure.WebJobs;
using System;
using System.IO;
using System.Linq;

namespace AlpineSkiHouse.WebJobs.WebJobs
{
    public class ImageAdjustments
    {
        public static void ResizeAndGrayscaleImage(
            [QueueTrigger("skicard-imageprocessing")] string message,
            [Blob("cardimages/{queueTrigger}", FileAccess.Read)] Stream imageStream,
            [Blob("processed/{queueTrigger}", FileAccess.Write)] Stream resizedImageStream)
        {
            var imageFactory = new ImageFactory();
            imageFactory.Load(imageStream)
                .Resize(new System.Drawing.Size(100, 0))
                .Filter(matrixFilter: ImageProcessor.Imaging.Filters.Photo.MatrixFilters.GreyScale)
                .Save(resizedImageStream);
        }
    }
}
