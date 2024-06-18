using Application.Interfaces;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CheckPaymentExisted(int paymentId)
        {
            return _unitOfWork.PaymentRepository.CheckPaymentMethodExisted(paymentId);
        }
    }
}
