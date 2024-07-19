using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Order;
using Application.ViewModels.OrderItem;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromotionService _promotionService;
        public PaymentService(IUnitOfWork unitOfWork, IPromotionService promotionService)
        {
            _unitOfWork = unitOfWork;
            _promotionService = promotionService;
        }

        public async Task<ServiceResponse<OrderDetailsViewModel>> ChangePaymentStatusForOrder(int orderid)
        {
            var response = new ServiceResponse<OrderDetailsViewModel>();
            try
            {
                var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(orderid);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order does not exist.";
                    return response;
                }

                order.PaymentId = 2;
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveChangeAsync();

                response.Success = true;
                response.Message = "Change payment method successfully.";
                response.Data = new OrderDetailsViewModel
                {
                    Id = order.Id,
                    UserName = order.Account.Name,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    DiscountPercentage = (_promotionService.GetDiscountPercentageForUser(order.AccountId)).Result,
                    PaymentName = order.Payment.PaymentMethod,
                    NumberItems = order.Items.Count,
                    OrderDate = order.CreatedDate.Value,
                    ShipDate = order.DeliveryDate,
                    Items = order.Items.Select(i => new OrderItemViewModel
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        ProductName = i.Product.Name,
                        Price = i.Price,
                        WarrantyDescription = _unitOfWork.ProductWarrantyRepository.GetWarrantyByItem(order.Id, i.ProductId).Result.Description,
                        WarrantyStartDate = _unitOfWork.ProductWarrantyRepository.GetWarrantyByItem(order.Id, i.ProductId).Result.StartDate,
                        WarrantyEndDate = _unitOfWork.ProductWarrantyRepository.GetWarrantyByItem(order.Id, i.ProductId).Result.EndDate
                    }).ToList()
                };

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