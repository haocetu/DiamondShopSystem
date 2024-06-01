using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Diamond : BaseEntity
    {
        public int OriginId {  get; set; }
        public int CaratWeightId {  get; set; }
        public int ColorId { get; set; }
        public int ClarityId { get; set; }
        public int CutId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        //Relationship
        public virtual Origin? Origin { get; set; }
        public virtual CaratWeight? CaratWeight { get; set; }
        public virtual Clarity? Claritys { get; set; }
        public virtual Cut? Cuts { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
