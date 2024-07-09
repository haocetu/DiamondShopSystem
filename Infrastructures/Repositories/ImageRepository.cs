using Application.Interfaces;
using Application.Repositories;
using Application.ViewModels.ImageDTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly AppDbContext _appDbContext;


        public ImageRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _appDbContext = context;
        }

		public List<string> GetImagesByProductId(int id)
		{
			//return _appDbContext.Images.Where(x => x.ProductId == id).ToList();
            return _appDbContext.Images.Where(x => x.ProductId == id).Select(x => x.UrlPath).ToList();
		}
        public async Task<IEnumerable<Image>>GetImagesByDiamondIdAsync(int id)
        {
            return await _appDbContext.Images.Where(x => x.DiamondId == id).ToListAsync();
        }
    }
}