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

		public async Task<IEnumerable<Diamond>> SearchProduct(string origin)
		{
			return await _dbContext.Diamonds.Where(u => u.OriginName.Contains(origin)).ToListAsync();
		}
	}
}
