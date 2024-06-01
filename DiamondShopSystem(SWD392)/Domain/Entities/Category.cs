using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        //Realationship
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
