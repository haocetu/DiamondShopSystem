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
		public decimal Size { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public decimal Wage { get; set; }
		[Required]
		public int ProductTypeId { get; set; }
		[Required]
		public int CategoryId { get; set; }
		[Required]
		public int Quantity { get; set; }
		public List<IFormFile> ProductImages { get; set; } = [];
	}
}