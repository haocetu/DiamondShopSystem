using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public string? Name {  get; set; }
        //Relationship
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
