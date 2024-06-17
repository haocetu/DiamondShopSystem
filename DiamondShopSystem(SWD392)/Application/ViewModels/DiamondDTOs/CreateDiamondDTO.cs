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
        public string ClarityName { get; set; }
        [Required]
        public string CutName { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public decimal Price { get; set; }
        public List<IFormFile> DiamondImages { get; set; } = new List<IFormFile>();

    }
}
