using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext _context;
        protected DbSet<Image> _dbSet;

        public ImageRepository(AppDbContext context)
        {
            _dbSet = context.Set<Image>();
            _context = context;
        }

        public async Task AddAsync(Image entity)
        {
            await _dbSet.AddAsync(entity);            
        }

        public Task AddRangeAsync(List<Image> entities)
        {
            throw new NotImplementedException();
        }

        public Task<List<Image>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Image?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void SoftRemove(Image entity)
        {
            throw new NotImplementedException();
        }

        public void SoftRemoveRange(List<Image> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Image entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<Image> entities)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Image>> SearchImageURLByDiamondIdAsync(int DiamondId)
        {
            return await _context.Images.Where(u => u.DiamondId == DiamondId).ToListAsync();
        } 
        public async Task<IEnumerable<Image>> SearchImageURLByProductIdAsync(int ProductId)
        {
            return await _context.Images.Where(u => u.DiamondId == ProductId).ToListAsync();
        }
    }
}
