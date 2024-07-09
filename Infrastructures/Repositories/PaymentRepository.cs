using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckPaymentMethodExisted(int paymentMethodId)
        {
            return await _context.Payments.FindAsync(paymentMethodId) != null;
        }
    }
}
