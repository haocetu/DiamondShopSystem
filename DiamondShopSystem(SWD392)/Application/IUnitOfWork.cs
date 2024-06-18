using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }
        public IDiamondRepository DiamondRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IImageRepository ImageRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
