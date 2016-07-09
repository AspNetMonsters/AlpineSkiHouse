using AlpineSkiHouse.Data;
using AlpineSkiHouse.Events;
using AlpineSkiHouse.Models;
using MediatR;
using System;

namespace AlpineSkiHouse.Handlers
{
    public class AddSkiPassOnPurchaseCompleted : INotificationHandler<PurchaseCompleted>
    {
        private readonly PassContext _passContext;

        public AddSkiPassOnPurchaseCompleted(PassContext passContext)
        {
            _passContext = passContext;
        }

        public void Handle(PurchaseCompleted notification)
        {
            foreach (var passPurchase in notification.PassPurchases)
            {
                //TODO: Refactor to a AddPass command? Still want to do all passes in a single transaction though...
                Pass pass = new Pass
                {
                    CardId = passPurchase.CardId,
                    CreatedOn = DateTime.UtcNow,
                    PassTypeId = passPurchase.PassTypeId
                };
                _passContext.Passes.Add(pass);
                //TODO: Raise PassAdded event??
            }
            _passContext.SaveChanges();
        }
    }
}
