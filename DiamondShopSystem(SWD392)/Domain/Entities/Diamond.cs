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
        public string OriginName {  get; set; }
        public float CaratWeight {  get; set; }
        public DiamondClarity ClarityName { get; set; }
        public DiamondCut CutName { get; set; }
        public DiamondColor Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<ProductDiamond> ProductDiamonds { get; set; } = [];
        public List<Image> Images { get; set; }
    }
}
