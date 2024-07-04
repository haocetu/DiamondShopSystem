using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiamondShopSystem_SWD392_.Controllers
{
	public class CategoryController : BaseController
	{
		private readonly ICategoryService categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await categoryService.GetCategoriesAsync();
			return Ok(categories);
		}
	}
}