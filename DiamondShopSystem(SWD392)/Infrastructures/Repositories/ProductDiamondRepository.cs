using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
	public class ProductDiamondRepository : GenericRepository<ProductDiamond>, IProductDiamondRepository
	{
		private readonly AppDbContext _dbContext;
		public ProductDiamondRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
		{
			_dbContext = context;
		}
	}
}
