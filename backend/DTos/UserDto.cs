using backend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTos
{
    public class UserDto : User
    {
        public IFormFile file { get; set; }
    }
}
