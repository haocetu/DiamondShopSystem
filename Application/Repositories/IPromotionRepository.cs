using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
	public interface IPromotionRepository : IGenericRepository<Promotion>
	{
		Task<bool> HardDelete(int id);
		Task<bool> IsExisted(int point, decimal percent);
		Task<bool> IsValid(int point, decimal percent);
	}
}