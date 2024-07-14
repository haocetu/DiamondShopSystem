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
        public string Origin { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public decimal CaratWeight { get; set; }
        public DateTime DateOfIssue {  get; set; }
        public Diamond Diamond {  get; set; }

    }
}
