using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTos
{
    public class followerDto
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userImage { get; set; }
        public bool isFollowed { get; set; }
    }
}
