using Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _dbContext;
        public PaymentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CheckPaymentMethodExisted(int paymentId)
        {
            return await _dbContext.Payments.AnyAsync(p=>p.Id == paymentId);
        }
    }
}
