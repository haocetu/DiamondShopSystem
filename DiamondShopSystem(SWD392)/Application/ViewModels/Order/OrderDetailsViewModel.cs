using Application.ViewModels.OrderItem;

namespace Application.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public int PaymentId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public ICollection<OrderItemViewModel> Items { get; set; }
    }
}
