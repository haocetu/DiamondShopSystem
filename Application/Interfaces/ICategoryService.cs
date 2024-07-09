using Application.Commons;
using Application.ViewModels.CategoryDTOs;
using Application.ViewModels.ProductDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface ICategoryService
	{
		Task<ServiceResponse<IEnumerable<CategoryDTO>>> GetCategoriesAsync();
		Task<ServiceResponse<CategoryDTO>> GetCategoryByIdAsync(int id);
		Task<ServiceResponse<CategoryDTO>> UpdateCategoryAsync(int id, CreateCategoryDTO cat);
		Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);
		Task<ServiceResponse<CategoryDTO>> CreateCategoryAsync(CreateCategoryDTO cat);
	}
}
