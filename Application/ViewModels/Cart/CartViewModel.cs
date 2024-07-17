using Application.ViewModels.CartItems;

namespace Application.ViewModels.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NumberItems { get; set; }
        public decimal TotalPrice { get; set; }
        public double DiscountPercentage { get; set; }
        public ICollection<CartItemViewModel> Items { get; set; }
    }

}
