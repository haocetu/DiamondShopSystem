using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public string Status {  get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } 
    }
}
