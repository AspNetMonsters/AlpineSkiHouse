using MediatR;
using System;

namespace AlpineSkiHouse.Events
{
    /// <summary>
    /// An event notification that occurs when a Ski Card has been scanned at a particular location
    /// </summary>
    public class CardScanned : INotification
    {
        public int CardId { get; set; }
        public int LocationId { get; set; }
    }
}
