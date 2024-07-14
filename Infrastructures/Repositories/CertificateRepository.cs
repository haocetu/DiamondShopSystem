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
    public class CertificateRepository : GenericRepository<Certificates>, ICertificateRepository
    {
        private readonly AppDbContext _appDbContext;
        public CertificateRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _appDbContext = context;
        }
        public async Task<bool> IsReportNumberExist(string reportNumber)
        {
            var exist = await _appDbContext.Certificates.AnyAsync(x => x.ReportNumber == reportNumber);
            return exist;
        }
    }
}
