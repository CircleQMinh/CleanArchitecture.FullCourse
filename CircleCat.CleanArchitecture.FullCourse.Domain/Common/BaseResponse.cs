using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Domain.Common
{
    public class BaseResponse<T> 
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public T? Result { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
