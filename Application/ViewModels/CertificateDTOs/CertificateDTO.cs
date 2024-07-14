using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CertificateDTOs
{
    public class CertificateDTO
    {
        public int Id { get; set; }
        public string ReportNumber { get; set; }
        public string Origin { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string DiamondCut { get; set; }
        public decimal CaratWeight { get; set; }
        public DateTime DateOfIssue { get; set; }
    }
}
