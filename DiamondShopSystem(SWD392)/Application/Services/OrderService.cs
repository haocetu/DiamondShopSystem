using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Order;
using Application.ViewModels.OrderItem;
using Domain.Entities;
using System.Data.Common;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        public OrderService(IUnitOfWork unitOfWork, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
        }

        public async Task<ServiceResponse<OrderViewModel>> ChangeOrderStatusAsync(int orderid, bool status)
        {
            var response = new ServiceResponse<OrderViewModel>();
            try
            {
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

        public async Task<ServiceResponse<OrderViewModel>> PlaceOrderAsync()
        {
            var response = new ServiceResponse<OrderViewModel>();
            try
            {
                var cart = await _unitOfWork.CartRepository.GetCartForUserAsync(_claimsService.GetCurrentUserId.Value);
                if (cart == null)
                {
                    response.Success = false;
                    response.Message = "Cart does not exist";
                }

                var order = new Order
                {
                    AccountId = _claimsService.GetCurrentUserId.Value,
                    CreatedDate = DateTime.UtcNow,
                    TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity),
                    Items = cart.Items.Select(i => new OrderItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList(),
                    CreatedBy = _claimsService.GetCurrentUserId.Value,
                    IsDeleted = false,
                    Status = "New Order",
                    PaymentId = 1,
                    ShipDate = DateTime.UtcNow.AddDays(10),
                };

                await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CartRepository.DeleteCartItem(cart.Items);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CartRepository.DeleteCart(cart.Id);
                await _unitOfWork.SaveChangeAsync();

                response.Data = new OrderViewModel
                {
                    Id = order.Id,
                    UserId = order.AccountId,
                    OrderDate = order.CreatedDate.Value,
                    TotalAmount = order.TotalPrice,
                    Items = order.Items.Select(i => new OrderItemViewModel
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                };
                response.Success = true;
                response.Message = "Order created successfully.";
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
