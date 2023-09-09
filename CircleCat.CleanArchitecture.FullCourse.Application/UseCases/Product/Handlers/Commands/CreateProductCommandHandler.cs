using AutoMapper;
using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Requests.Commands;
using CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Validators;
using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using CircleCat.CleanArchitecture.FullCourse.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Entities = CircleCat.CleanArchitecture.FullCourse.Domain.Entities; // using alias
namespace CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResponse<ProductDTO>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public CreateProductCommandHandler(IMediator mediator, IMapper mapper, IProductService productService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<BaseResponse<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductValidator();
            var validationResult = await validator.ValidateAsync(request.Dto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            var entity = _mapper.Map<Entities.Product>(request.Dto);
            var result = await _productService.Add(entity);
            return new BaseResponse<ProductDTO>
            {
                Success = true,
                Result = _mapper.Map<ProductDTO>(result)
            };
        }
    }
}
