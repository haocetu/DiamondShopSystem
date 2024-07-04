using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
	public interface IProductDiamondRepository : IGenericRepository<ProductDiamond>
	{
		List<int> GetPrimaryDiamondsByProductId(int id);
		List<int> GetSubDiamondsByProductId(int id);
	}
}
