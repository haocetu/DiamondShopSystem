namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> CheckPaymentExisted(int paymentId);
    }
}
