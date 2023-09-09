using AutoMapper;
using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Requests.Queries;
using CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using CircleCat.CleanArchitecture.FullCourse.Domain.Exceptions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = CircleCat.CleanArchitecture.FullCourse.Domain.Entities; // using alias
using System.Linq.Expressions;
using CircleCat.CleanArchitecture.FullCourse.Domain.Entities;
using CircleCat.CleanArchitecture.FullCourse.Domain.Common;

namespace CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Handlers.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, BaseResponse<List<ProductDTO>>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public GetProductsQueryHandler(IMediator mediator, IMapper mapper, IProductService productService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productService = productService;

        }
        public async Task<BaseResponse<List<ProductDTO>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetProductsValidator();
            var validationResult = await validator.ValidateAsync(request.Dto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
                //return new BaseResponse<List<ProductDTO>>
                //{
                //    Success = false,
                //    Message = "Validation Error",
                //    Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList()
                //};
            }
            //prepare params
            Expression<Func<Entities.Product, bool>> filterExpression = q =>
            (request.Dto.CategoryId <= 0 || q.CategoryId == request.Dto.CategoryId) &&
            q.Name.Contains(request.Dto.Name);

            Func<IQueryable<Entities.Product>, IOrderedQueryable<Entities.Product>> orderBy = q => q.OrderBy(x => x.Id);

            List<string> includesProperties = new() { nameof(Entities.Product.Category) };

            PaginationFilter paginationFilter = new() { PageNumber = request.Dto.PageNumber, PageSize = request.Dto.PageSize };

            //execute
            var result = await _productService.GetAllAsync(filterExpression, orderBy, includesProperties, paginationFilter);
            var totalItems = await _productService.CountAsync(filterExpression);
            return new BaseResponse<List<ProductDTO>>
            {
                Success = true,
                Result = _mapper.Map<List<ProductDTO>>(result),
                PageSize = request.Dto.PageSize,
                PageNumber = request.Dto.PageNumber,
                TotalItems = totalItems
            };
        }
    }
}
