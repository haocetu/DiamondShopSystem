using Application;
using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly IDiamondRepository _diamondRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IPaymentRepository _paymentRepository;
        public UnitOfWork(AppDbContext dbContext, IAccountRepository accountRepository, IDiamondRepository diamondRepository, IOrderRepository orderRepository, IImageRepository imageRepository, IPaymentRepository paymentRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _diamondRepository = diamondRepository;
            _orderRepository = orderRepository;
            _imageRepository = imageRepository;
            _paymentRepository = paymentRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;
        public IDiamondRepository DiamondRepository => _diamondRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public IImageRepository ImageRepository => _imageRepository;
        public IPaymentRepository PaymentRepository => _paymentRepository;
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
