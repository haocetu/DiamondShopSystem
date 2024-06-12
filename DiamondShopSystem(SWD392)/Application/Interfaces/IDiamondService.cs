using Application.Commons;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.DiamondDTOs;
using Application.ViewModels.ImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDiamondService
    {
        Task<ServiceResponse<IEnumerable<DiamondDTO>>> GetDiamondAsync();
        Task<ServiceResponse<DiamondDTO>> GetDiamondByIdAsync(int id);
        Task<ServiceResponse<DiamondDTO>> UpdateDiamondAsync(int id, UpdateDiamondDTO diamondDTO);
        Task<ServiceResponse<bool>> DeleteDiamondAsync(int id);
        Task<ServiceResponse<IEnumerable<DiamondDTO>>> SearchDiamondByOriginAsync(string origin);
        Task<ServiceResponse<DiamondDTO>> CreateDiamondAsync(CreateDiamondDTO createdDiamondDTO);
        Task<ServiceResponse<ImageDiamondDTO>> AddImageDiamondById(int id, ImageDiamondDTO imageDiamondDTO);
    }
}
