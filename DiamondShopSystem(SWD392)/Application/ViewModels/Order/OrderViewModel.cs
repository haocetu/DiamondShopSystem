using Application.ViewModels.OrderItem;

namespace Application.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PromotionId { get; set; }
        public ICollection<OrderItemViewModel> Items { get; set; }
    }
}
