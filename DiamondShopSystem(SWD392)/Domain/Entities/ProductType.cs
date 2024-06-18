using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public string Material { get; set; }
        public float Weight { get; set; }
        public decimal Price { get; set; }
    }
}
