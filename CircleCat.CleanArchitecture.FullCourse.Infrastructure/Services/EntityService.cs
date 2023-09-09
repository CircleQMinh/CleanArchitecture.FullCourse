using AutoMapper;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Repositories;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services;
using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Infrastructure.Services
{
    public class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        protected IMapper _mapper;
        protected IGenericRepository<T> _repository;
        public EntityService(IMapper mapper,IGenericRepository<T> repository)
        {
            _mapper= mapper;
            _repository= repository;
        }
        public async Task<T> Add(T entity)
        {
            await _repository.Insert(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<T>> AddRange(ICollection<T> entities)
        {
            await _repository.InsertRange(entities);
            await _repository.SaveChangesAsync();
            return entities;
        }

        public async Task<int> CountAsync()
        {
            return await _repository.GetCount();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null)
        {
            return await _repository.GetCount(expression);
        }

        public async Task Delete(int id)
        {
            await (_repository.Delete(id));
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> Exist(int id)
        {
            var result = await _repository.Get(q => q.Id == id);
            return result!=null;
        }

        public async Task<bool> Exist(Expression<Func<T, bool>> expression)
        {
            var result = await _repository.Get(expression);
            return result != null;
        }

        public async Task<T> FindAsync(int id)
        {
            return await _repository.Get(q=>q.Id== id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            return await _repository.Get(expression,includes);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _repository.GetAll(null,null,null,null);
        }


        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, PaginationFilter paginationFilter = null)
        {
            return await _repository.GetAll(expression, orderBy, includes, paginationFilter);
        }

        public async Task<T> Update(T entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }
    }
}
