using Application.Commons;
using Application.Interfaces;
using Application.RequestModel.Cart;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.Cart;
using Application.ViewModels.CartItems;
using Domain.Entities;
using System.Data.Common;
using System.Security.Principal;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        public CartService(IUnitOfWork unitOfWork, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
        }
        public async Task<ServiceResponse<CartViewModel>> GetCartForUserAsync()
        {
            var response = new ServiceResponse<CartViewModel>();
            try
            {
                var cart = await _unitOfWork.CartRepository.GetCartForUserAsync(_claimsService.GetCurrentUserId.Value);
                response.Data = new CartViewModel
                {
                    Id = cart.Id,
                    UserId = cart.AccountId,
                    Items = cart.Items.Select(i => new CartItemViewModel
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                };
                response.Success = true;
                response.Message = "This is cart for user.";
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

        public async Task<ServiceResponse<CartViewModel>> AddOrUpdateCartAsync(CartRequestModel cartRequest)
        {
            var response = new ServiceResponse<CartViewModel>();

            try
            {
                var cart = await _unitOfWork.CartRepository.GetCartForUserAsync(_claimsService.GetCurrentUserId.Value);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        AccountId = _claimsService.GetCurrentUserId.Value,
                        Items = new List<CartItem>()
                    };
                    foreach (var item in cartRequest.Items)
                    {
                        var price = (await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId)).Price;
                        cart.Items.Add(new CartItem
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = price
                        });
                    }
                    await _unitOfWork.CartRepository.AddAsync(cart);
                    var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                    if (isSuccess)
                    {
                        response.Success = true;
                        response.Message = "Add to cart successfully.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Error to add item to cart.";
                    }
                }
                else
                {
                    foreach (var item in cartRequest.Items)
                    {
                        var price = (await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId)).Price;
                        cart.Items.Add(new CartItem
                        {
                            CartId = cart.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = price
                        });
                    }
                    _unitOfWork.CartRepository.Update(cart);
                    var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                    if (isSuccess)
                    {
                        response.Success = true;
                        response.Message = "Add to cart successfully.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Error to add item to cart.";
                    }
                }
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

        public async Task<ServiceResponse<bool>> DeleteCartAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var cart = _unitOfWork.CartRepository.GetByIdAsync(id);
                if (cart == null)
                {
                    response.Success = false;
                    response.Message = "Cart is not existed";
                    return response;
                }
                await _unitOfWork.CartRepository.DeleteCartAsync(id);
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
