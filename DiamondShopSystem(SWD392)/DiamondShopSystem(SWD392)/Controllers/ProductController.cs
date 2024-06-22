using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiamondShopSystem_SWD392_.Controllers
{
	public class ProductController : BaseController
	{
		private readonly IProductService productService;
		public ProductController(IProductService productService)
		{
			this.productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var products = await productService.GetProductsAsync();
			return Ok(products);
		}
	}
}