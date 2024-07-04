using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Order;
using Application.ViewModels.OrderItem;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IPromotionService _promotionService;
        public OrderService(IUnitOfWork unitOfWork, IClaimsService claimsService, IPromotionService promotionService)
        {
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _promotionService = promotionService;
        }

        public async Task<ServiceResponse<OrderViewModel>> ChangeOrderStatusAsync(int orderid, string status)
        {
            var response = new ServiceResponse<OrderViewModel>();
            try
            {
                var userid = _claimsService.GetCurrentUserId;
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderid);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order does not exist.";
                    return response;
                }

                order.Status = status;
                order.ModifiedBy = userid;
                order.ModifiedDate = DateTime.Now;

                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveChangeAsync();

            }
            catch (DbUpdateException ex)
            {
                response.Success = false;
                response.Message = "Failed to update data.";
                response.ErrorMessages = new List<string> { ex.Message };
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

                var userpoint = await _unitOfWork.AccountRepository.GetPoint(_claimsService.GetCurrentUserId.Value);

                var order = new Order
                {
                    AccountId = _claimsService.GetCurrentUserId.Value,
                    CreatedDate = DateTime.UtcNow,
                    TotalPrice = _promotionService.DiscountFromPoint(cart.Items.Sum(i => i.Price * i.Quantity), userpoint),
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

                await _unitOfWork.AccountRepository.UpdatePoint(_claimsService.GetCurrentUserId.Value, order.TotalPrice);
                await _unitOfWork.SaveChangeAsync();

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
                    PromotionId = _promotionService.GetPromotionIdFromPoint(userpoint),
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
            catch (DbUpdateException ex)
            {
                response.Success = false;
                response.Message = "Failed to update data.";
                response.ErrorMessages = new List<string> { ex.Message };
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
