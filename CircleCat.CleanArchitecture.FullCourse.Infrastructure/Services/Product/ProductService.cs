using AutoMapper;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Repositories;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = CircleCat.CleanArchitecture.FullCourse.Domain.Entities; // using alias
namespace CircleCat.CleanArchitecture.FullCourse.Infrastructure.Services.Product
{
    public class ProductService : EntityService<Entities.Product>, IProductService
    {
        public ProductService(IMapper mapper, IGenericRepository<Entities.Product> repository) : base(mapper, repository)
        {

        }

        public async Task<Entities.Product> GetRandomProduct()
        {
            var random = new Random();
            var max = await _repository.GetCount();
            if (!(max > 0))
            {
                return null;
            }
            var id = random.Next(1,max);
            return await _repository.Get(q=>q.Id == id);
        }
    }
}
