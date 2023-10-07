using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend.DAL
{
    public interface IJWTManage
    {
        public string GenerateToken(string email);
        public ClaimsPrincipal GetPrincipal(string token);
    }
}
