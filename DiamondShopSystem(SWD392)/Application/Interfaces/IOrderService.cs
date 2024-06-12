using Application.Commons;
using Application.ViewModels.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOrdersAsync();
        Task<ServiceResponse<OrderDTO>> GetOrderByIdAsync(int orderId);
        Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOrderByUserIDAsync(int userId);
        Task<ServiceResponse<bool>> CancelOrderAsync(int id);
        Task<ServiceResponse<OrderDTO>> CreateOrderAsync(CreateOrderDTO order);

    }
}
