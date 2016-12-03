using MediatR;

namespace AlpineSkiHouse.Events
{
    /// <summary>
    /// An event notification that occurs when a scan has occurred
    /// </summary>
    public class CardScanned : INotification
    {
        public int ScanId { get; set; }
    }
}
