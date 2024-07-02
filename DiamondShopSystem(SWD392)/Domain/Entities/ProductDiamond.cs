using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductDiamond : BaseEntity
    {
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public int DiamondId { get; set; }

        //Relationship
        public Product Product { get; set; }
        public Diamond Diamond { get; set; }
    }
}
