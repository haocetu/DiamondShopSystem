using Application.Commons;
using Application.ViewModels.PromotionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPromotionService
    {
        Task<ServiceResponse<IEnumerable<PromotionDTO>>> GetListPromotionAsync();
        Task<ServiceResponse<PromotionDTO>> GetPromotionByIdAsync(int id);
        Task<ServiceResponse<PromotionDTO>> CreatePromotionAsync(CreatePromotionDTO newPromotion);
        Task<ServiceResponse<bool>> DeletePromotionAsync(int id);
        decimal DiscountFromPoint(decimal price, int point);
    }
}
