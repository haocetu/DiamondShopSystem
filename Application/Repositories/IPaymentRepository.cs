using Domain.Entities;

namespace Application.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> CheckPaymentMethodExisted(int id);
        Task<Payment> GetPaymentById(int id);
    }
}
