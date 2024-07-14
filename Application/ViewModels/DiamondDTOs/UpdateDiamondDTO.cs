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
        public float? CaratWeight { get; set; }
        [EnumDataType(typeof(DiamondOrigin))]
        public string? OriginName { get; set; }

        [EnumDataType(typeof(DiamondClarity))]
        public string? ClarityName { get; set; }
        [EnumDataType(typeof(DiamondCut))]
        public string? CutName { get; set; }
        [EnumDataType(typeof(DiamondColor))]
        public string? Color { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public List<IFormFile> UpdateImages { get; set; } = [];

    }
}
