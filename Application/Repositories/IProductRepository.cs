﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		Task<IEnumerable<Product>> SearchProduct(string search);
		Task<bool> ProductTypeInUsed(int id);
	}
}
