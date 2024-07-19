using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiamondShopSystem_SWD392_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPatch("change-payment-method")]
        [Authorize(Roles = "SaleStaff,Admin")]
        public async Task<IActionResult> ChangePaymentStatusForOrder(int orderid)
        {
            var result = await _paymentService.ChangePaymentStatusForOrder(orderid);
            return Ok(result);  
        }
    }
}
