using Application.ViewModels.OrderItem;

namespace Application.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public float DiscountPercentage { get; set; }
        public decimal TotalPrice { get; set; }
        public int NumberItems { get; set; }
        public string PaymentName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public ICollection<OrderItemViewModel> Items { get; set; }
    }
}
