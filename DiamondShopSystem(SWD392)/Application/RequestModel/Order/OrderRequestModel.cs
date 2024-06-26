using Application.RequestModel.OrderItem;

namespace Application.RequestModel.Order
{
    public class OrderRequestModel
    {
        public ICollection<OrderItemRequestModel> Items { get; set; }
    }
}
