using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.UseCases.Product.Validators
{
    public class CreateProductValidator : AbstractValidator<ProductCreateDTO>
    {
        public CreateProductValidator() { 
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters");
            RuleFor(q => q.Price).NotNull().GreaterThan(1000);
            RuleFor(q=>q.CategoryId).NotNull().GreaterThan(0);

        }
    }
}
