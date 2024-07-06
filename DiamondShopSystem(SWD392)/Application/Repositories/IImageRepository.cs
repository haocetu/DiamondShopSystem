using Application.ViewModels.ImageDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IImageRepository : IGenericRepository<Image>
    {
		List<string> GetImagesByProductId(int id);
        Task<IEnumerable<Image>> GetImagesByDiamondIdAsync(int id);
    }
}
