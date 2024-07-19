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
	public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
	{
		private readonly AppDbContext _dbContext;
		public PromotionRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
		{
			_dbContext = context;
		}
		public async Task<bool> HardDelete(int id)
		{
			var promotion = await _dbContext.Promotions.FindAsync(id);
			if (promotion == null)
			{
				return false;
			}
			_dbContext.Promotions.Remove(promotion);
			return true;
		}
		public async Task<bool> IsExisted(int point, decimal percent)
		{
			var result = await _dbContext.Promotions.AnyAsync(x => x.Point == point || x.DiscountPercentage == percent);
			if (result)
			{
				return true;
			}
			return false;
		}
		public async Task<bool> IsValid(int point, decimal percent)
		{
			var promotions = await _dbContext.Promotions.OrderBy(x => x.Point).ToListAsync();
			Promotion lowPro;
			Promotion highPro;
			if (promotions != null)
			{
				if (point < promotions[0].Point)
				{
					if (percent < promotions[0].DiscountPercentage) return true;
					return false;
				}
				if (point > promotions[promotions.Count - 1].Point)
				{
					if (percent > promotions[promotions.Count - 1].DiscountPercentage) return true;
					return false;
				}
				for (int i = 0; i < promotions.Count - 1; i++)
				{
					if (promotions[i].Point < point && promotions[i + 1].Point > point)
					{
						lowPro = promotions[i];
						highPro = promotions[i + 1];
						if (lowPro.DiscountPercentage < percent && percent < highPro.DiscountPercentage) return true;
						return false;
					}
				}
			}
			return true;
		}
	}
}