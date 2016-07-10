namespace AlpineSkiHouse.Events
{
    public class PassPurchased
    {
        public int PassTypeId { get; set; }
        public int CardId { get; set; }

        public decimal PricePaid { get; set; }

        public string DiscountCode { get; set; }
    }
}