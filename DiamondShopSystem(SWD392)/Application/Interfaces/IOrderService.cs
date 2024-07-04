using Application.Commons;
using Application.ViewModels.Order;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderViewModel>> PlaceOrderAsync();
        Task<ServiceResponse<OrderViewModel>> ChangeOrderStatusAsync(int orderid, bool status);
    }
}
