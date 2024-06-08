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
    public class DiamondRepository : GenericRepository<Diamond>,IDiamondRepository
    {
        private readonly AppDbContext _dbContext;
        public DiamondRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Diamond>> SearchDiamondByOriginAsync(string origin)
        {
            return await _dbContext.Diamonds.Where(u => u.OriginName.Contains(origin)).ToListAsync();
        }
    }
}
