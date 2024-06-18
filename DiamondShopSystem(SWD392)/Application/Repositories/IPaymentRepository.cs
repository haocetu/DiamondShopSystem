using Domain.Entities;

namespace Application.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> CheckPaymentMethodExisted(int paymentId);    
    }
}
