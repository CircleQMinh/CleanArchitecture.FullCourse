using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Validators
{
    public class GetProductsValidator : AbstractValidator<ProductSearchDTO>
    {
        public GetProductsValidator()
        {
            RuleFor(q => q.Name).NotNull();
            RuleFor(q => q.CategoryId).NotNull();
            RuleFor(q => q.PageNumber).GreaterThan(0);
            RuleFor(q => q.PageSize).GreaterThanOrEqualTo(5);
        }
    }
}
