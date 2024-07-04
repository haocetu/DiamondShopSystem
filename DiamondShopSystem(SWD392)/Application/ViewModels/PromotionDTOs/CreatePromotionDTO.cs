using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PromotionDTOs
{
    public class CreatePromotionDTO
    {
        [Required]
        public int Point {  get; set; }
        [Required]
        public decimal DiscountPercentage {  get; set; }
    }
}
