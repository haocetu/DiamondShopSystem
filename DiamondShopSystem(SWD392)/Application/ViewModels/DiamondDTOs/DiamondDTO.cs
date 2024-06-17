using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DiamondDTOs
{
    public class DiamondDTO
    {
        public int Id {  get; set; }
        public string OriginName { get; set; }
        public float CaratWeight { get; set; }
        public string ClarityName { get; set; }
        public string CutName { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
       // public List<Image> ImageURL { get; set; } = [];
    }
}
