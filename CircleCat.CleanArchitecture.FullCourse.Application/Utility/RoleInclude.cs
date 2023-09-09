using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.Utility
{
    public static class RoleInclude
    {
        public static string Roles(string[] roles)
        {
            return String.Join(",", roles);
        }
    }
}
