using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void UpdateRange(List<T> entities);
        void SoftRemove(T entity);
        Task AddRangeAsync(List<T> entities);
        void SoftRemoveRange(List<T> entities);

        Task DeleteRangeAsync(IEnumerable<T> entities);



    }
}
