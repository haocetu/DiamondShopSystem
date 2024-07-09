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
            return await _dbContext.Orders.Include(o => o.Items)
                                        .Include(o=>o.Account)
                                        .Include(o=>o.Payment)
                                        .Include(o=>o.ProductWarranties)
                                        .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _dbContext.Orders.Include(o => o.Items)
                                        .Include(o => o.Account)
                                        .Include(o => o.Payment)
                                        .Include(o => o.ProductWarranties)
                                        .ToListAsync();
        }

        public async Task<List<Order>> GetOrderByUserIDAsync(int userId)
        {
            var result = await _dbContext.Orders.Include(o => o.Items)
                                        .Include(o => o.Account)
                                        .Include(o => o.Payment)
                                        .Include(o => o.ProductWarranties)
                                        .Where(o => o.AccountId == userId)
                                        .ToListAsync();
            return result;
        }
    }
}
