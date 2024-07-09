using Application.Commons;
using Application.ViewModels.CategoryDTOs;
using Application.ViewModels.ProductTypeDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IProductTypeService
	{
		Task<ServiceResponse<IEnumerable<ProductTypeDTO>>> GetProductTypesAsync();
		Task<ServiceResponse<ProductTypeDTO>> GetProductTypeByIdAsync(int id);
		Task<ServiceResponse<ProductTypeDTO>> UpdateProductTypeAsync(int id, CreateProductTypeDTO t);
		Task<ServiceResponse<bool>> DeleteProductTypeAsync(int id);
		Task<ServiceResponse<ProductTypeDTO>> CreateProductTypeAsync(CreateProductTypeDTO t);
	}
}