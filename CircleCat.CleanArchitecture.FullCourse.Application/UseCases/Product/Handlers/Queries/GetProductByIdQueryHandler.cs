using AutoMapper;
using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Requests.Queries;
using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using CircleCat.CleanArchitecture.FullCourse.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = CircleCat.CleanArchitecture.FullCourse.Domain.Entities; // using alias
namespace CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Handlers.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, BaseResponse<ProductDTO>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public GetProductByIdQueryHandler(IMediator mediator, IMapper mapper, IProductService productService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productService = productService;
        }
        public async Task<BaseResponse<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            var entity = await _productService.FindAsync(q => q.Id == request.Id, new List<string> { nameof(Entities.Product.Category) });
            if (entity == null)
            {
                throw new NotFoundException(nameof(Entities.Product), request.Id);
                //return new BaseResponse<ProductDTO>
                //{
                //    Success = false,
                //    Message = "Not Found Error",
                //};
            }
            return new BaseResponse<ProductDTO>
            {
                Success = true,
                Result = _mapper.Map<ProductDTO>(entity)
            };

        }
    }
}
