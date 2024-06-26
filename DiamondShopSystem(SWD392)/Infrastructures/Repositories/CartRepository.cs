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
        public async Task DeleteCartAsync(int cartId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c=>c.Id == cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Cart> GetCartForUserAsync(int userId)
        {
            return await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.AccountId == userId);
        }
    }
}
