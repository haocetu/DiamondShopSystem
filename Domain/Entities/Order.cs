namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string Status {  get; set; }
        public float DiscountPercentage { get; set; }
        public decimal TotalPrice { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public List<OrderItem> Items { get; set; }
        public List<ProductWarranty> ProductWarranties { get; set; }
    }
}
