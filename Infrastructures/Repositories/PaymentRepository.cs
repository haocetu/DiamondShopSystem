using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckPaymentMethodExisted(int id)
        {
            return await _context.Payments.FindAsync(id) != null;
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                return payment;
            }
            return null;
        }
    }
}
