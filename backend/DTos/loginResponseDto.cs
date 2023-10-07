using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTos
{
    public class loginResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string profilePic { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        public string Token { get; set; }
    }
}
