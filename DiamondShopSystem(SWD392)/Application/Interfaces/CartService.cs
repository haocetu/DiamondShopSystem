using Application.Commons;
using Application.ViewModels.CartItems;
using System.Data.Common;

namespace Application.Interfaces
{
    internal class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<List<CartItemViewModel>>> GetCartItemsForUser(int accountId)
        {
            var response = new ServiceResponse<List<CartItemViewModel>> ();
            try
            {
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
                if (account == null)
                {
                    response.Success = false;
                    response.Message = "Account is not existed.";
                    return response;
                }
                var result = await _unitOfWork.CartRepository.GetCartItemsForUser(accountId);
                
                response.Success = true;
                response.Message = "This is your cart";
                response.Data = result;

                return response;    
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
