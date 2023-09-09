using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Requests.Commands;
using CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Requests.Queries;
using CircleCat.CleanArchitecture.FullCourse.Application.Utility;
using CircleCat.CleanArchitecture.FullCourse.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CircleCat.CleanArchitecture.FullCourse.API.Controllers
{
    [Authorize(Roles = SystemRole.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] ProductSearchDTO dto)
        {
            var result = await _mediator.Send(new GetProductsQuery(dto));
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDTO dto)
        {
            var result = await _mediator.Send(new CreateProductCommand(dto));
            return Ok(result);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDTO dto)
        {
            var result = await _mediator.Send(new UpdateProductCommand(id, dto));
            return Ok(result);
        }
        [Authorize(Roles = SystemRole.Administrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(result);
        }
    }
}
