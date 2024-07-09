using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly AppDbContext _dbContext;
		public ProductRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
		{
			_dbContext = context;
		}

		public async Task<IEnumerable<Product>> SearchProduct(string search)
		{
			return await _dbContext.Products.Where(u => u.Name.Contains(search)).ToListAsync();
		}
	}
}
