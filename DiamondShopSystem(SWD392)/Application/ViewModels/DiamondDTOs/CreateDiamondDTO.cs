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
    public class CreateDiamondDTO
    {
        [Required]
        public string OriginName { get; set; }
        [Required]
        public float CaratWeight { get; set; }
        [Required]
        [EnumDataType(typeof(DiamondClarity))]
        public string ClarityName { get; set; }
        [Required]
        [EnumDataType(typeof(DiamondCut))]
        public string CutName { get; set; }
        [Required]
        [EnumDataType(typeof(DiamondColor))]
        public string Color { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public List<IFormFile> DiamondImages { get; set; } = [];

    }
}
