using Application.Commons;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.DiamondDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDiamondService
    {
        Task<ServiceResponse<IEnumerable<Product>>> GetDiamondAsync();
        Task<ServiceResponse<Product>> GetDiamondByIdAsync(int id);
        Task<ServiceResponse<Product>> UpdateDiamondAsync(int id, UpdateDiamondDTO diamondDTO);
        Task<ServiceResponse<bool>> DeleteDiamondAsync(int id);
        Task<ServiceResponse<IEnumerable<Product>>> SearchDiamondByOriginAsync(string origin);
        Task<ServiceResponse<Product>> CreateDiamondAsync(CreateDiamondDTO createdDiamondDTO);

    }
}
