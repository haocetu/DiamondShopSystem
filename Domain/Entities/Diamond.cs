using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Diamond : BaseEntity
    {
        public string Name { get; set; }
        public string Origin {  get; set; }
        public float CaratWeight {  get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<ProductDiamond> ProductDiamonds { get; set; } = [];
        public List<Image> Images { get; set; }
    }
}
