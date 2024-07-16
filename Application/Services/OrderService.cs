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

        public async Task<ServiceResponse<OrderDetailsViewModel>> ChangeOrderStatusAsync(int orderid, string status)
        {
            var response = new ServiceResponse<OrderDetailsViewModel>();
            try
            {
                var user = await _unitOfWork.AccountRepository.GetByIdAsync(_claimsService.GetCurrentUserId.Value); ;

                var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(orderid);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order does not exist.";
                    return response;
                }

                order.Status = status;
                order.ModifiedBy = user.Id;
                order.ModifiedDate = DateTime.Now;

                _unitOfWork.OrderRepository.Update(order);

                if (status == "Finished")
                {
                    await _unitOfWork.ProductWarrantyRepository.CreateWarrantyByOrderId(orderid);
                }

                await _unitOfWork.SaveChangeAsync();

                response.Success = true;
                response.Message = "Change order status successfully.";
                response.Data = new OrderDetailsViewModel
                {
                    Id = order.Id,
                    UserName = user.Name,
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
                        Price = i.Price
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

        public async Task<ServiceResponse<OrderDetailsViewModel>> GetOrderDetailsAsync(int orderid)
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

                response.Success = true;
                response.Message = "This is details information for order";
                response.Data = new OrderDetailsViewModel
                {
                    Id = order.Id,
                    UserName = order.Account.Name,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    PaymentName = order.Payment.PaymentMethod,
                    DiscountPercentage = (_promotionService.GetDiscountPercentageForUser(order.AccountId)).Result,
                    NumberItems = order.Items.Count,
                    OrderDate = order.CreatedDate.Value,
                    ShipDate = order.DeliveryDate,
                    Items = order.Items.Select(i => new OrderItemViewModel
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                };
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

        public async Task<ServiceResponse<List<OrderDetailsViewModel>>> GetOrdersForUserAsync()
        {
            var response = new ServiceResponse<List<OrderDetailsViewModel>>();
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetOrderByUserIDAsync(_claimsService.GetCurrentUserId.Value);
                var discount = await _promotionService.GetDiscountPercentageForUser(_claimsService.GetCurrentUserId.Value);

                var result = orders.Select(order => new OrderDetailsViewModel
                {
                    Id = order.Id,
                    UserName = order.Account.Name,
                    Status = order.Status,
                    DiscountPercentage = discount,
                    TotalPrice = order.TotalPrice,
                    NumberItems = order.Items.Count,
                    PaymentName = order.Payment.PaymentMethod,
                    OrderDate = order.CreatedDate.Value,
                    ShipDate = order.DeliveryDate,
                    Items = order.Items.Select(i => new OrderItemViewModel
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                }).ToList();

                if (result.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Orders retrieved successfully";
                    response.Data = result;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have any orders";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }

        public async Task<ServiceResponse<List<OrderDetailsViewModel>>> GetOrdersAsync()
        {
            var response = new ServiceResponse<List<OrderDetailsViewModel>>();
            try
            {
                var user = await _unitOfWork.AccountRepository.GetByIdAsync(_claimsService.GetCurrentUserId.Value); ;

                var orders = await _unitOfWork.OrderRepository.GetOrdersAsync();

                var result = orders.Select(order => new OrderDetailsViewModel
                {
                    Id = order.Id,
                    UserName = order.Account.Name,
                    Status = order.Status,
                    DiscountPercentage = (_promotionService.GetDiscountPercentageForUser(order.AccountId)).Result,
                    TotalPrice = order.TotalPrice,
                    NumberItems = order.Items.Count,
                    PaymentName = order.Payment.PaymentMethod,
                    OrderDate = order.CreatedDate.Value,
                    ShipDate = order.DeliveryDate,
                    Items = order.Items.Select(i => new OrderItemViewModel
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                }).ToList();

                if (result.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Orders retrieved successfully";
                    response.Data = result;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have any orders";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }

        public async Task<ServiceResponse<OrderViewModel>> PlaceOrderAsync(string? address, string? phonenumber)
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

                var user = await _unitOfWork.AccountRepository.GetByIdAsync(_claimsService.GetCurrentUserId.Value);
                var userpoint = await _unitOfWork.AccountRepository.GetPoint(user.Id);

                var deliveryAddress = !string.IsNullOrEmpty(address) ? address : user.Address;
                var receiverPhoneNumber = !string.IsNullOrEmpty(phonenumber) ? phonenumber : user.PhoneNumber;

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
                    DeliveryAddress = deliveryAddress,
                    ReceiverPhoneNumber = receiverPhoneNumber,
                    Status = "New Order",
                    PaymentId = 1,
                    DeliveryDate = DateTime.UtcNow.AddDays(10),
                };

                await _unitOfWork.AccountRepository.UpdatePoint(_claimsService.GetCurrentUserId.Value, order.TotalPrice);
                await _unitOfWork.SaveChangeAsync();

                foreach (var item in cart.Items)
                {
                    var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
                    if (product != null)
                    {
                        product.Quantity -= item.Quantity;
                        _unitOfWork.ProductRepository.Update(product);
                    }
                }

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
                    UserName = order.Account.Name,
                    OrderDate = order.CreatedDate.Value,
                    DeliveryAddress = deliveryAddress,
                    ReceiverPhoneNumber = receiverPhoneNumber,
                    TotalAmount = order.TotalPrice,
                    NumberItems = order.Items.Count,
                    Items = order.Items.Select(i => new OrderItemViewModel
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                };
                response.Success = true;
                response.Message = "Order created successfully.";
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
