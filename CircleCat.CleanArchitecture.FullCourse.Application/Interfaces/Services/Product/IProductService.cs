using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = CircleCat.CleanArchitecture.FullCourse.Domain.Entities; // using alias
namespace CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services.Product
{
    public interface IProductService : IEntityService<Entities.Product> 
    {
        Task<Entities.Product> GetRandomProduct();
    }
}
