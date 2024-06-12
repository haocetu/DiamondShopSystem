using Application.Interfaces;
using Application.Services;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.OrderDTOs;
using Microsoft.AspNetCore.Mvc;

namespace DiamondShopSystem_SWD392_.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderList()
        {
            var order = await _orderService.GetOrdersAsync();
            return Ok(order);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (!order.Success)
            {
                return NotFound(order);
            }
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createdOrderDTO)
        {
            //Dòng này kiểm tra xem dữ liệu đầu vào (trong trường hợp này là createdAccountDTO)
            //đã được kiểm tra tính hợp lệ bằng các quy tắc mô hình (model validation) hay chưa.
            //Nếu dữ liệu hợp lệ, nó tiếp tục kiểm tra và xử lý.
            if (ModelState.IsValid)
            {
                var response = await _orderService.CreateOrderAsync(createdOrderDTO);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                return BadRequest("Invalid request data.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var deletedUser = await _orderService.CancelOrderAsync(id);
            if (!deletedUser.Success)
            {
                return NotFound(deletedUser);
            }

            return Ok(deletedUser);
        }
    }
}
