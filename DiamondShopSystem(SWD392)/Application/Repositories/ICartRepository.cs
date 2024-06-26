using Domain.Entities;

namespace Application.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task DeleteCartAsync(int cartId);
        Task<Cart> GetCartForUserAsync(int userId);
    }
}
