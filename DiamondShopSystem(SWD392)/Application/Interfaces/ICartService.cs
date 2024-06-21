using Application.Commons;
using Application.ViewModels.CartItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartItemViewModel>>> GetCartItemsForUser(int accountId);
    }
}
