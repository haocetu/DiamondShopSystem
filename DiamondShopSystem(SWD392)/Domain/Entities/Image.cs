using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string UrlPath { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int DiamondId { get; set; }
        public Diamond Diamond { get; set; }
    }
}
