using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductTypeDTOS
{
	public class CreateProductTypeDTO
	{
		[Required]
		public string Material { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
		public decimal Price { get; set; }
	}
}