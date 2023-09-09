using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Category;
using CircleCat.CleanArchitecture.FullCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product
{
    public class ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; } = new CategoryDTO();
    }
}
