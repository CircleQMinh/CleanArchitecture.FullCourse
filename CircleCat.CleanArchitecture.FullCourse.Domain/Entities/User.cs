using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Domain.Entities
{
    public class User : IdentityUser 
    {
        public string SecureRandomNumber { get; set; } = string.Empty;
    }
}
