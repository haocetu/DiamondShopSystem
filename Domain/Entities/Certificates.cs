using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Certificates : BaseEntity
    {
        public string ReportNumber { get; set; }
        public DiamondOrigin Origin { get; set; }
        public DiamondColor Color { get; set; }
        public DiamondClarity Clarity { get; set; }
        public DiamondCut DiamondCut { get; set; }
        public decimal CaratWeight { get; set; }
        public DateTime DateOfIssue {  get; set; }

    }
}
