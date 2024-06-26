using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _dbContext;
        public OrderRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _dbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderId);
        }
        public async Task<List<Order>> GetOrderByUserIDAsync(int userId)
        {
            var result = await _dbContext.Orders.Where(o => o.AccountId == userId).ToListAsync();
            return result;
        }
    }
}
