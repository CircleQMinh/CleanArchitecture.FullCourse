using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product;
using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Requests.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<BaseResponse<ProductDTO>>
    {
    }
}
