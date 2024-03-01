using Restaurant.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurant.Application.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void UpdateRange(List<T> entities);
        void SoftRemove(T entity);
        Task AddRangeAsync(List<T> entities);
        void SoftRemoveRange(List<T> entities);

       /* Task<Pagination<T>> ToPagination(int pageNumber = 0, int pageSize = 10);*/
    }
}
