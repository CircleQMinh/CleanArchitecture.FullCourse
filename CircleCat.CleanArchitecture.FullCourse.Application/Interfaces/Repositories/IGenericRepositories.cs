using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null);
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, PaginationFilter paginationFilter = null);
        Task<int> GetCount(Expression<Func<T, bool>> expression = null);
        Task<double> GetMax(Expression<Func<T, bool>> expression = null, Expression<Func<T, decimal>> prop = null);
        Task<double> GetMin(Expression<Func<T, bool>> expression = null, Expression<Func<T, decimal>> prop = null);
        Task<double> GetAverage(Expression<Func<T, bool>> expression = null, Expression<Func<T, decimal>> prop = null);
        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
        Task<bool> SaveChangesAsync();
    }
}
