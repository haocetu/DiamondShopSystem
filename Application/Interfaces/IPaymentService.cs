using Application.Commons;
using Application.ViewModels.Order;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<ServiceResponse<OrderDetailsViewModel>> ChangePaymentStatusForOrder(int orderid);
    }
}
