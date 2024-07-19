using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _context = context;
        }
        public async Task DeleteCart(int cartId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }
        }
        public async Task<Cart> GetCartForUserAsync(int userId)
        {
            var result = await _context.Carts.Include(c => c.Items)
                                        .ThenInclude(ci => ci.Product)
                                        .FirstOrDefaultAsync(c => c.AccountId == userId);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task DeleteCartItem(List<CartItem> items)
        {
            foreach (var item in items)
            {
                _context.CartItems.Remove(item);
            }
        }
    }
}
