using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cut : BaseEntity
    {
        public string? Name { get; set; }
        public long Price { get; set; }
        //Relationship
        public virtual ICollection<Diamond> Diamonds { get; set; } = new List<Diamond>();

    }
}
