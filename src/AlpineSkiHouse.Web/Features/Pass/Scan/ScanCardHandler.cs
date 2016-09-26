using AlpineSkiHouse.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Features.Pass.Scan
{
    public class ScanCardHandler : IAsyncRequestHandler<ScanCardCommand, ScanCardResult>
    {
        private readonly ICardValidator _validator;
        private readonly IMediator _mediator;
        public ScanCardHandler(ICardValidator validator, IMediator mediator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public Task<ScanCardResult> Handle(ScanCardCommand message)
        {
            await _mediator.PublishAsync(new CardScanned { CardId = message.CardId, LocationId = message.LocationId, DateTime = message.DateTime });
            return Task.FromResult(new ScanCardResult { CardIsValid = _validator.IsValid(message.CardId, message.LocationId) });
        }
    }
}
