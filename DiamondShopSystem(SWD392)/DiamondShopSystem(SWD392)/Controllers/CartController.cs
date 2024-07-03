using Application.Interfaces;
using Application.RequestModel.Cart;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("view-cart")]
        [Authorize]
        public async Task<IActionResult> GetCartItemsForUser()
        {
            var result = await _cartService.GetCartForUserAsync();
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessages);
            }
            return Ok(result);
        }

        [HttpPatch("add-to-cart")]
        [Authorize]
        public async Task<IActionResult> AddOrUpdateCartAsync(CartRequestModel request)
        {
            var result = await _cartService.AddOrUpdateCartAsync(request);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessages);
            }
            return Ok();
        }

        [HttpDelete("delete-cart/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCartAsync(int id)
        {
            var result = await _cartService.DeleteCartAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessages);
            }
            return Ok();
        }
    }
}
