using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public float Size { get; set; }
        public float Lenght { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
