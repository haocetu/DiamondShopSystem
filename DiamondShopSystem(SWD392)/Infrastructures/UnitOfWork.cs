using Application;
using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly IDiamondRepository _diamondRepository;
        public UnitOfWork(AppDbContext dbContext, IAccountRepository accountRepository, IDiamondRepository diamondRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _diamondRepository = diamondRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;
        public IDiamondRepository DiamondRepository => _diamondRepository;
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
