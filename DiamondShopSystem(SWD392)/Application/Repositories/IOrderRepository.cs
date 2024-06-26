using Domain.Entities;

namespace Application.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetOrderByUserIDAsync(int userId);
    }
}
