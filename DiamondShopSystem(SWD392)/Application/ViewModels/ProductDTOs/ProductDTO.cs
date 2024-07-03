using Application.ViewModels.CategoryDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductDTOs
{
	public class ProductDTO : BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public decimal Wage { get; set; }
		public int ProductTypeId { get; set; }
		public int CategoryId { get; set; }
		public CategoryDTO CategoryDTO { get; set; }
		public int Quantity { get; set; }
		public List<string> Images { get; set; }
	}
}