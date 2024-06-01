using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public decimal? Size { get; set; }
        public string? ImageUrl { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
        public int? DiamondId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Wage { get; set; }
        public int? WarrantyDocumentsId { get; set; }
        //Relationship
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public virtual Category? Category { get; set; }
        public virtual WarrantyDocument? WarrantyDocument { get; set;}
        public virtual Diamond? Diamond { get; set; }
    }
}
