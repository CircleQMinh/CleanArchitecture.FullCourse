using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Repositories;
using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using CircleCat.CleanArchitecture.FullCourse.Infrastructure.Persistence.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Infrastructure.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericRepository(AppDbContext databaseContext, IHttpContextAccessor httpContextAccessor)
        {
            context = databaseContext;
            db = context.Set<T>();
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = db;
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, PaginationFilter paginationFilter = null)
        {
            IQueryable<T> query = db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (paginationFilter == null)
            {
                paginationFilter = new PaginationFilter();
            }

            return await query.AsNoTracking().Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
               .Take(paginationFilter.PageSize)
               .ToListAsync();
        }

        public async Task<double> GetAverage(Expression<Func<T, bool>> expression = null, Expression<Func<T, decimal>> prop = null)
        {
            IQueryable<T> query = db;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return (double)await query.AverageAsync(prop);
        }

        public async Task<int> GetCount(Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = db;
            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query.CountAsync();
        }

        public async Task<double> GetMax(Expression<Func<T, bool>> expression = null, Expression<Func<T, decimal>> prop = null)
        {
            IQueryable<T> query = db;
            if (expression != null)
            {
                query = query.Where(expression);
            }

            return (double)await query.MaxAsync(prop);
        }

        public async Task<double> GetMin(Expression<Func<T, bool>> expression = null, Expression<Func<T, decimal>> prop = null)
        {
            IQueryable<T> query = db;
            if (expression != null)
            {
                query = query.Where(expression);
            }

            return (double)await query.MinAsync(prop);
        }

        public async Task Insert(T entity)
        {
            await db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            db.Attach(entity);

            context.Entry(entity).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            var entity = await db.FindAsync(id);
            if (entity != null)
            {
                db.Remove(entity);
            }

        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            db.RemoveRange(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            return await context.SaveChangesAsync(username) > 0;
        }

    }
}
