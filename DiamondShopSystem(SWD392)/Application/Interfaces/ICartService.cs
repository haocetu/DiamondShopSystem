using Application.Commons;
using Application.RequestModel.Cart;
using Application.ViewModels.Cart;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Task<ServiceResponse<CartViewModel>> GetCartForUserAsync();
        Task<ServiceResponse<CartViewModel>> AddOrUpdateCartAsync(CartRequestModel request);
        Task<ServiceResponse<bool>> DeleteCartAsync(int id);
    }
}
