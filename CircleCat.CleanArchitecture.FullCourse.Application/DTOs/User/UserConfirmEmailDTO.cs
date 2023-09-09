using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.DTOs.User
{
    public class UserConfirmEmailDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
