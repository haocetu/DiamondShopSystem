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
        private readonly IAccountRepository _accountRepository;
        private readonly IDiamondRepository _diamondRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IProductDiamondRepository _productDiamondRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IPromotionRepository _promotionRepository;


        public UnitOfWork(AppDbContext dbContext, IAccountRepository accountRepository, IDiamondRepository diamondRepository, ICartRepository cartRepository,
                          ICategoryRepository categoryRepository, IImageRepository imageRepository, IOrderRepository orderRepository, IPaymentRepository paymentRepository,
                          IProductDiamondRepository productDiamondRepository, IProductRepository productRepository, IProductTypeRepository productTypeRepository,
                          IPromotionRepository promotionRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _diamondRepository = diamondRepository;
            _cartRepository = cartRepository;
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _productDiamondRepository = productDiamondRepository;
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _promotionRepository = promotionRepository;
        }

        public IAccountRepository AccountRepository  => _accountRepository;
        public IDiamondRepository DiamondRepository => _diamondRepository;
        public ICartRepository CartRepository => _cartRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IImageRepository ImageRepository => _imageRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public IPaymentRepository PaymentRepository => _paymentRepository;
        public IProductDiamondRepository ProductDiamondRepository => _productDiamondRepository;
        public IProductRepository ProductRepository => _productRepository;
        public IPromotionRepository PromotionRepository => _promotionRepository;
        public IProductTypeRepository ProductTypeRepository => _productTypeRepository;
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
