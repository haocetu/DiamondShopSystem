using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Diamond : BaseEntity
    {
        public string OriginName {  get; set; }
        public float CaratWeight {  get; set; }
        public string ClarityName { get; set; }
        public string CutName { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public List<ProductDiamond> ProductDiamonds { get; set; } = [];
        public List<Image> Images { get; set; }
    }
}
