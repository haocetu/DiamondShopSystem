using Domain.Enums;
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
        [EnumDataType(typeof(DiamondClarity))]
        public string? ClarityName { get; set; }
        [EnumDataType(typeof(DiamondCut))]
        public string? CutName { get; set; }
        [EnumDataType(typeof(DiamondColor))]
        public string? Color { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
