using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WarrantyDocument : BaseEntity
    {
        public int? Period { get; set; }
        public string? TermsAndConditions { get; set; }
        //Relationship
        public virtual Product Products { get; set; }
    }
}
