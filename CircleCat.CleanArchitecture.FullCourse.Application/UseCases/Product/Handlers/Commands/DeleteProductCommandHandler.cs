using AutoMapper;
using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Requests.Commands;
using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using CircleCat.CleanArchitecture.FullCourse.Domain.Entities;
using CircleCat.CleanArchitecture.FullCourse.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = CircleCat.CleanArchitecture.FullCourse.Domain.Entities; // using alias
namespace CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse<ProductDTO>>
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public DeleteProductCommandHandler(IMediator mediator, IMapper mapper, IProductService productService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productService = productService;
        }
        public async Task<BaseResponse<ProductDTO>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var exist = await _productService.Exist(request.Id);
            if (!exist)
            {
                throw new NotFoundException(nameof(Entities.Product), request.Id);
                //return new BaseResponse<ProductDTO>
                //{
                //    Success = false,
                //    Message = "Not Found Error",
                //};
            }
            await _productService.Delete(request.Id);
            return new BaseResponse<ProductDTO>
            {
                Success = true,
            };

        }
    }
}
