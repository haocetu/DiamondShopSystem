using Application.Commons;
using Application.ViewModels.ProductDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IProductService
	{
		Task<ServiceResponse<IEnumerable<Product>>> GetProductsAsync();
		//Task<ServiceResponse<Product>> GetProductByIdAsync(int id);
		//Task<ServiceResponse<Product>> UpdateProductAsync(int id, CreateProductDTO pro);
		//Task<ServiceResponse<bool>> DeleteProductAsync(int id);
		//Task<ServiceResponse<IEnumerable<Product>>> SearchProductAsync(string search);
		//Task<ServiceResponse<Product>> CreateProductAsync(CreateProductDTO pro);
	}
}
