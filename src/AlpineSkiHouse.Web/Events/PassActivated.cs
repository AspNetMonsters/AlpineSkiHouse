using MediatR;

namespace AlpineSkiHouse.Events
{ 
    public class PassActivated : INotification
    {
        public int PassActivationId { get; set; }
    }
}
