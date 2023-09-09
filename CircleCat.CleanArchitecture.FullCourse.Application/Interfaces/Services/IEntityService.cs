using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services
{
    public interface IEntityService<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, PaginationFilter paginationFilter = null);

        Task<T> FindAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> expression, List<string> includes = null);

        Task<bool> Exist(int id);
        Task<bool> Exist(Expression<Func<T, bool>> expression);
        Task<T> Add(T entity);
        Task<ICollection<T>> AddRange(ICollection<T> entities);
        Task<T> Update(T entity);
        Task Delete(int id);

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression = null);
    }
}
