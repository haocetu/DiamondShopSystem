using Application.Commons;
using Application.RequestModel.Cart;
using Application.ViewModels.Cart;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Task<ServiceResponse<CartViewModel>> GetCartForUserAsync();
        Task<ServiceResponse<CartViewModel>> AddToCartAsync(CartRequestModel request);
        Task<ServiceResponse<CartViewModel>> RemoveFromCartAsync(CartRequestModel request);
        Task<ServiceResponse<bool>> DeleteCartAsync();
    }
}
