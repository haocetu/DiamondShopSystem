using Domain.Entities;

namespace Application.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        void DeleteCart(int cartId);
        void DeleteCartItem(List<CartItem> items);
        Task<Cart> GetCartForUserAsync(int userId);
    }
}
