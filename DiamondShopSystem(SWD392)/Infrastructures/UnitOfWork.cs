using Application;
using Application.Interfaces;
using Application.Repositories;
using Infrastructures.Repositories;
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
        private ICurrentTime _currentTime;
        private IClaimsService _claimsService;

        public UnitOfWork(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService)
        {
            _dbContext = dbContext;
            _currentTime = currentTime;
            _claimsService = claimsService;
        }

        private IDiamondRepository _diamondRepository;
        public IDiamondRepository DiamondRepository
        {
            get
            {
                if (_diamondRepository is null)
                {
                    _diamondRepository = new DiamondRepository(_dbContext, _currentTime, _claimsService);
                }
                return _diamondRepository;
            }
        }

        private IAccountRepository _accountRepository;
        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository is null)
                {
                    _accountRepository = new AccountRepository(_dbContext, _currentTime, _claimsService);
                }
                return _accountRepository;
            }
        }

        private IOrderRepository _orderRepository;
        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository is null)
                {
                    _orderRepository = new OrderRepository(_dbContext, _currentTime, _claimsService);
                }
                return _orderRepository;
            }
        }
        private IImageRepository _imageRepository;
        public IImageRepository ImageRepository
        {
            get
            {
                if (_imageRepository is null)
                {
                    _imageRepository = new ImageRepository(_dbContext, _currentTime, _claimsService);
                }
                return _imageRepository;
            }
        }
        private IPaymentRepository _paymentRepository;
        public IPaymentRepository PaymentRepository
        {
            get
            {
                if (_paymentRepository is null)
                {
                    _paymentRepository = new PaymentRepository(_dbContext);
                }
                return _paymentRepository;
            }
        }

        private ICartRepository _cartRepository;
        public ICartRepository CartRepository
        {
            get
            {
                if (_cartRepository is null)
                {
                    _cartRepository = new CartRepository(_dbContext);
                }
                return _cartRepository;
            }
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
