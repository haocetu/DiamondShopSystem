using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment 
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public List<Order> Orders { get; set; }
    }
}
