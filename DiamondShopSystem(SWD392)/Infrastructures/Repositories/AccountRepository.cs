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
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly AppDbContext _dbContext;
        public AccountRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public Task<bool> CheckEmailNameExited(string email) =>
                                                 _dbContext.Accounts.AnyAsync(u => u.Email == email);

        public Task<bool> CheckPhoneNumberExited(string phonenumber) =>
                                                _dbContext.Accounts.AnyAsync(u => u.PhoneNumber == phonenumber);

        public async Task<int> GetPoint(int userid)
        {
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(user => user.Id == userid);
            if (user != null)
            {
                return user.Point;
            }
            return 0;
        }

        public async Task<Account> GetUserByConfirmationToken(string token)
        {
            return await _dbContext.Accounts.SingleOrDefaultAsync(
                u => u.ConfirmationToken == token
            );
        }

        public async Task<Account> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await _dbContext.Accounts.Include(u=>u.Role).FirstOrDefaultAsync(record => record.Email == email
                                                                && record.Password == password);
            if (user is null)
            {
                throw new Exception("Email & password is not correct");
            }

            return user;
        }

        public async Task<IEnumerable<Account>> SearchAccountByNameAsync(string name)
        {
            return await _dbContext.Accounts.Where(u => u.Name.Contains(name)).ToListAsync();
        }

        public async Task UpdatePoint(int userid, decimal price)
        {
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(u => u.Id == userid);

            if (user != null)
            {
                int newPoint = (int)Math.Round(price / 1_000_000m, 0);
                user.Point += newPoint;
            }
        }
    }
}
