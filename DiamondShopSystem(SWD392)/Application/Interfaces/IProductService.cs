using Application.Commons;
using Application.ViewModels.DiamondDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IProductService
	{
		Task<ServiceResponse<IEnumerable<DiamondDTO>>> GetProductAsync();
		Task<ServiceResponse<DiamondDTO>> GetProductByIdAsync(int id);
		Task<ServiceResponse<DiamondDTO>> UpdateProductAsync(int id, UpdateDiamondDTO diamondDTO);
		Task<ServiceResponse<bool>> DeleteProductAsync(int id);
		Task<ServiceResponse<IEnumerable<DiamondDTO>>> SearchProductByOriginAsync(string origin);
		Task<ServiceResponse<DiamondDTO>> CreateProductAsync(CreateDiamondDTO createdDiamondDTO);
	}
}
