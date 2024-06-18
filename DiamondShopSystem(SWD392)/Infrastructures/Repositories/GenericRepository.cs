using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Infrastructures.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected DbSet<T> _dbSet;
        private readonly ICurrentTime _timeService;
        private readonly IClaimsService _claimsService;

        public GenericRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService)
        {
            _dbSet = context.Set<T>();
            _timeService = timeService;
            _claimsService = claimsService;
        }
        public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                // todo should throw exception when not found
                return result;

            }catch (Exception ex)
            {
                throw new Exception(" Not Find by ID");
            }
            
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = _timeService.GetCurrentTime();
            entity.CreatedBy = _claimsService.GetCurrentUserId.Value;
            await _dbSet.AddAsync(entity);
        }

        public void SoftRemove(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedBy = _claimsService.GetCurrentUserId.Value;
            _dbSet.Update(entity);
        }

        public void Update(T entity)
        {
            entity.ModifiedDate = _timeService.GetCurrentTime();
            entity.ModifiedBy = _claimsService.GetCurrentUserId.Value;
            _dbSet.Update(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.GetCurrentUserId.Value;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public void SoftRemoveRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletedDate = _timeService.GetCurrentTime();
                entity.DeletedBy = _claimsService.GetCurrentUserId.Value;
            }
            _dbSet.UpdateRange(entities);
        }
        public Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }
        //public async Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10)
        //{
        //    var itemCount = await _dbSet.CountAsync();
        //    var items = await _dbSet.OrderByDescending(x => x.CreationDate)
        //                            .Skip(pageIndex * pageSize)
        //                            .Take(pageSize)
        //                            .AsNoTracking()
        //                            .ToListAsync();

        //    var result = new Pagination<TEntity>()
        //    {
        //        PageIndex = pageIndex,
        //        PageSize = pageSize,
        //        TotalItemsCount = itemCount,
        //        Items = items,
        //    };

        //    return result;
        //}

        public void UpdateRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.GetCurrentUserId.Value;
            }
            _dbSet.UpdateRange(entities);
        }
        //public async Task UpdateAsync(TEntity entity)
        //{
        //    // Cập nhật thông tin thời gian và người dùng
        //    entity.ModificationDate = _timeService.GetCurrentTime();
        //    entity.ModificationBy = _claimsService.GetCurrentUserId;

        //    // Đánh dấu thực thể là đã được sửa đổi
        //    _dbSet.Update(entity);

        //    // Lưu các thay đổi vào cơ sở dữ liệu
        //    await _dbSet.SaveChangesAsync();
        //}
    }

}
