using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Wage { get; set; }
        public int ProductTypeId { get; set; }
        public int CategoryId { get; set; }
 
        //Relationship
        public ProductType ProductType { get; set; }
        public Category Category { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<ProductDiamond> ProductDiamonds { get; set; }
        public List<ProductWarranty> ProductWarranties { get; set; }
        public List<Image> Images { get; set; }
    }
}
