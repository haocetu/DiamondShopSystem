using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiamondShopSystem_SWD392_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("{accountId}")]
        public async Task<IActionResult> GetCartItemsForUser(int accountId)
        {
            var result = await _cartService.GetCartItemsForUser(accountId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessages);
            }
            return Ok(result);
        }
    }
}
