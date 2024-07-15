using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductDTOs
{
	public class CreateProductDTO
	{
		[Required]
		public string Name { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 0.")]
		public int CategoryId { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "ProductTypeId must be greater than 0.")]
		public int ProductTypeId { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
		public decimal Weight { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Wage must be greater than 0.")]
		public decimal Wage { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
		public int Quantity { get; set; }
		public List<int> PrimaryDiamonds { get; set; }
		public List<int> SubDiamonds { get; set; }
		public List<IFormFile> ProductImages { get; set; } = [];
	}
}