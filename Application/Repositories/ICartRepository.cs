using Domain.Entities;

namespace Application.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task DeleteCart(int cartId);
        Task DeleteCartItem(List<CartItem> items);
        Task<Cart> GetCartForUserAsync(int userId);
    }
}
