using Application.RequestModel.CartItem;

namespace Application.RequestModel.Cart
{
    public class CartRequestModel
    {
        public ICollection<CartItemRequestModel> Items { get; set; }
    }
}
