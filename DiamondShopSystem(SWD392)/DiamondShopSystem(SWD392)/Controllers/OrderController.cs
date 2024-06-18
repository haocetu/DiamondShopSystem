using Application.Interfaces;
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
        
        [HttpGet("order/user/{id}")]
        public async Task<IActionResult> GetOrderByUserIDAsync(int id)
        {
            var order = await _orderService.GetOrderByUserIDAsync(id);
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
