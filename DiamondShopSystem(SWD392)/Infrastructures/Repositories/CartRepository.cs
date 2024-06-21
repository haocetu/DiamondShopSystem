using Application.Repositories;
using Application.ViewModels.CartItems;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItemViewModel>> GetCartItemsForUser(int accountId)
        {
            var cartItems = await _context.CartItems
                .Where(ci => ci.Cart.AccountId == accountId) 
                .Select(ci => new CartItemViewModel
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToListAsync();

            return cartItems;
        }
    }
}
