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
using Entities = CircleCat.CleanArchitecture.FullCourse.Domain.Entities; // using alias
namespace CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseResponse<ProductDTO>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public UpdateProductCommandHandler(IMediator mediator, IMapper mapper, IProductService productService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<BaseResponse<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductValidator();
            var validationResult = await validator.ValidateAsync(request.Dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
                //return new BaseResponse<ProductDTO>
                //{
                //    Success = false,
                //    Message = "Validation Error",
                //    Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList()
                //};
            }

            var entity = await _productService.FindAsync(q => q.Id == request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Entities.Product), request.Id);
                //return new BaseResponse<ProductDTO>
                //{
                //    Success = false,
                //    Message = "Not Found Error",
                //    Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList()
                //};
            }
            _mapper.Map(request.Dto, entity);
            var result = await _productService.Update(entity);
            return new BaseResponse<ProductDTO>
            {
                Success = true,
                Result = _mapper.Map<ProductDTO>(result)
            };
        }
    }
}
