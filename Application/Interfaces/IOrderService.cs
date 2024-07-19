using Application.Commons;
using Application.ViewModels.Order;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderViewModel>> PlaceOrderAsync(string? address, string? phonenumber, int paymentid);
        Task<ServiceResponse<OrderDetailsViewModel>> ChangeOrderStatusAsync(int orderid, string status);
        Task<ServiceResponse<List<OrderDetailsViewModel>>> GetOrdersAsync();
        Task<ServiceResponse<OrderDetailsViewModel>> GetOrderDetailsAsync(int orderid);
        Task<ServiceResponse<List<OrderDetailsViewModel>>> GetOrdersForUserAsync();
    }
}
