using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductWarrantyDTOs
{
	public class ProductWarrantyDTO
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int OrderId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Description { get; set; }
	}
}
