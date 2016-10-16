using AlpineSkiHouse.Data;
using AlpineSkiHouse.Events;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Services;
using MediatR;
using System;
using System.Collections.Generic;

namespace AlpineSkiHouse.Handlers
{
    public class AddSkiPassOnPurchaseCompleted : INotificationHandler<PurchaseCompleted>
    {
        private readonly PassContext _passContext;
        private readonly IMediator _bus;
        private readonly IDateService _dateService;

        public AddSkiPassOnPurchaseCompleted(PassContext passContext, IMediator bus, IDateService dateService)
        {
            _dateService = dateService;
            _passContext = passContext;
            _bus = bus;
        }

        public void Handle(PurchaseCompleted notification)
        {
            var newPasses = new List<Pass>();
            foreach (var passPurchase in notification.Passes)
            {
                Pass pass = new Pass
                {
                    CardId = passPurchase.CardId,
                    CreatedOn = _dateService.Now(),
                    PassTypeId = passPurchase.PassTypeId
                };
                newPasses.Add(pass);
            }

            _passContext.Passes.AddRange(newPasses);
            _passContext.SaveChanges();

            foreach (var newPass in newPasses)
            {
                var passAddedEvent = new PassAdded
                {
                    PassId = newPass.Id,
                    PassTypeId = newPass.PassTypeId,
                    CardId = newPass.CardId,
                    CreatedOn = newPass.CreatedOn
                };
                _bus.Publish(passAddedEvent);
            }

        }
    }
}
