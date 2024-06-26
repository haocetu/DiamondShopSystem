using Application.Commons;
using Application.RequestModel.Order;
using Application.ViewModels.Order;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderViewModel>> PlaceOrderAsync(OrderRequestModel request);
    }
}
