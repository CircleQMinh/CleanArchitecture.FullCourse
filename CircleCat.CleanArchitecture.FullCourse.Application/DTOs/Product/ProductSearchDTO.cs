using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product
{
    public class ProductSearchDTO
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;

    }
}
