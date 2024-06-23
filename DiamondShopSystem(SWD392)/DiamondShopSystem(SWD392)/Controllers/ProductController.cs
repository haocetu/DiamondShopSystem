using Application.Interfaces;
using Application.Services;
using Application.ViewModels.ProductDTOs;
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

		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromForm] CreateProductDTO createProduct)
		{

			if (ModelState.IsValid)
			{
				var response = await productService.CreateProductAsync(createProduct);
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
	}
}