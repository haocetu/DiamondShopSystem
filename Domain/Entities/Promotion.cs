using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Promotion : BaseEntity
    {
        public int Point { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
