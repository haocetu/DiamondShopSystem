using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CaratWeight : BaseEntity
    {
        public double Weight { get; set; }
        public decimal Price { get; set; }
        //Relationship
        public virtual ICollection<Diamond> Diamonds { get; set; } = new List<Diamond>();

    }
}
