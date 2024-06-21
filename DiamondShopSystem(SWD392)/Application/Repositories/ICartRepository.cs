using Application.ViewModels.CartItems;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartItemViewModel>> GetCartItemsForUser(int accountId);
    }
}
