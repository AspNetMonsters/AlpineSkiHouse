using AlpineSkiHouse.Data;
using AlpineSkiHouse.Events;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Web.Command;
using MediatR;

namespace AlpineSkiHouse.Web.Handlers
{
    public class ActivatePassHandler : IRequestHandler<ActivatePass, int>
    {
        private readonly PassContext _passContext;
        private readonly IMediator _mediator;

        public ActivatePassHandler(PassContext passContext, IMediator mediator)
        {
            _passContext = passContext;
            _mediator = mediator;
        }

        public int Handle(ActivatePass message)
        {
            PassActivation activation = new PassActivation
            {
                PassId = message.PassId,
                ScanId = message.ScanId
            };
            _passContext.PassActivations.Add(activation);
            _passContext.SaveChanges();

            _mediator.Publish(new PassActivated { PassActivationId = activation.Id });

            return activation.Id;
        }
    }
}
