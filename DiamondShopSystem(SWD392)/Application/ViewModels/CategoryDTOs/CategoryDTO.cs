﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CategoryDTOs
{
	public class CategoryDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public float Size { get; set; }
		public float Length { get; set; }
		public bool IsDeleted { get; set; }
	}
}