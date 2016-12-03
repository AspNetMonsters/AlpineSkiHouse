using AlpineSkiHouse.Data;
using AlpineSkiHouse.Events;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Services;
using MediatR;

namespace AlpineSkiHouse.Handlers
{
    public class LogCardScanned : INotificationHandler<CardScanned>
    {
        private readonly PassContext _passContext;
        private readonly IDateService _dateService;

        public LogCardScanned(PassContext passContext, IDateService dateService)
        {
            _dateService = dateService;
            _passContext = passContext;
        }

        public void Handle(CardScanned notification)
        {
            _passContext.Scans.Add(
                new Scan
                {
                    CardId = notification.CardId,
                    LocationId = notification.LocationId,
                    DateTime = _dateService.Now()
                });
            _passContext.SaveChanges();
        }
    }
}
