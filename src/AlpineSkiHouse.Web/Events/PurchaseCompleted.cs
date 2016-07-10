using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Events
{
    public class PurchaseCompleted : INotification
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string TransactionId { get; set; }

        public decimal TotalCost { get; set; }

        public List<PassPurchased> Passes { get; set; }        
    }
}
