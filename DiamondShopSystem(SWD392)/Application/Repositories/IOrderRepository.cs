using Domain.Entities;

namespace Application.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetOrderByUserIDAsync(int accountId);
    }
}
