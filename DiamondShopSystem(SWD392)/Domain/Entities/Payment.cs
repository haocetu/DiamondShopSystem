using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string? Name { get; set; }
        public int? PaymentType { get; set; }
        //Relationship
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
