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
		public List<int> GetSubDiamondsByProductId(int id)
		{
			return _dbContext.ProductDiamonds.Where(x => x.ProductId == id & x.IsMain == false).Select(x => x.DiamondId).ToList();
		}
		public List<int> GetPrimaryDiamondsByProductId(int id)
		{
			return _dbContext.ProductDiamonds.Where(x => x.ProductId == id & x.IsMain == true).Select(x => x.DiamondId).ToList();
		}
	}
}
