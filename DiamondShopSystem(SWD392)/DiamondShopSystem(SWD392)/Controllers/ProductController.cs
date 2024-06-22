using Application.Interfaces;
using Application.Services;
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
	}
}