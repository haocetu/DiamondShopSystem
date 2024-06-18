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
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        //Task UpdateAsync(TEntity entity);
        void UpdateRange(List<T> entities);
        void SoftRemove(T entity);
        Task AddRangeAsync(List<T> entities);
        void SoftRemoveRange(List<T> entities);

        Task DeleteRangeAsync(IEnumerable<T> entities);

        //Task<Pagination<TEntity>> ToPagination(int pageNumber = 0, int pageSize = 10);


    }
}
