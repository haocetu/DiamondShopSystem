using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public int? AccountId { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? StatusId {  get; set; }
        public int? PaymentId { get; set; }
        //Relationship
        public virtual Account? Account { get; set; }
        public virtual Status? Status { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    }
}
