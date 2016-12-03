using AlpineSkiHouse.Data;
using AlpineSkiHouse.Events;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Services;
using AlpineSkiHouse.Web.Command;
using MediatR;

namespace AlpineSkiHouse.Handlers
{
    public class CreateScanHandler : IRequestHandler<CreateScan, int>
    {
        private readonly PassContext _passContext;
        private readonly IDateService _dateService;
        private readonly IMediator _mediator;

        public CreateScanHandler(PassContext passContext, IDateService dateService, IMediator mediator)
        {
            _dateService = dateService;
            _passContext = passContext;
            _mediator = mediator;
        }

        public int Handle(CreateScan message)
        {
            var scan = new Scan
            {
                CardId = message.CardId,
                LocationId = message.LocationId,
                DateTime = _dateService.Now()
            };
            _passContext.Scans.Add(scan);
            _passContext.SaveChanges();

            _mediator.Publish(new CardScanned { ScanId = scan.Id });            
            return scan.Id;
        }
    }
}
