using Application;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public OrderRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService, IUnitOfWork unitOfWork) : base(context, timeService, claimsService)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Order>> GetOrderByUserIDAsync(int accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                return null;
            }
            return await _dbContext.Orders.Where(o => o.AccountId == accountId).ToListAsync();
        }
    }
}
