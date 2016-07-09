using MediatR;

namespace AlpineSkiHouse.Events
{
    public class PassPurchase
    {
        public int PassTypeId { get; set; }
        public int CardId { get; set; }
    }
}