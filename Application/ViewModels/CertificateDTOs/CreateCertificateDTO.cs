using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CertificateDTOs
{
    public class CreateCertificateDTO
    {
        public string ReportNumber { get; set; }
        [Required]
        [EnumDataType(typeof(DiamondOrigin))]
        public string Origin { get; set; }
        [Required]
        [EnumDataType(typeof(DiamondColor))]
        public string Color { get; set; }
        [Required]
        [EnumDataType(typeof(DiamondClarity))]
        public string Clarity { get; set; }
        [Required]
        [EnumDataType(typeof(DiamondCut))]
        public string DiamondCut { get; set; }
        public decimal CaratWeight { get; set; }
        public DateTime DateOfIssue { get; set; }
    }
}
