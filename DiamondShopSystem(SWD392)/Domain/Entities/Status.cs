using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Status : BaseEntity
    {
        public int? AccountId { get; set; }
        public int? Name { get; set; }
        //Relationship
        public virtual Account? Account { get; set; }
        public virtual Order? Orders { get; set; }
    }
}
