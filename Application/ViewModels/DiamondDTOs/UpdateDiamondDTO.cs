using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DiamondDTOs
{
    public class UpdateDiamondDTO
    {
        public string? OriginName { get; set; }
        public float? CaratWeight { get; set; }
        public DiamondClarity? ClarityName { get; set; }
        public DiamondCut? CutName { get; set; }
        public DiamondColor? Color { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public List<IFormFile> UpdateImages { get; set; } = [];

    }
}
