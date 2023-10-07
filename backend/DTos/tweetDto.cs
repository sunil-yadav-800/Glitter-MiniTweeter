using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTos
{
    public class tweetDto
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string message { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userImage { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool isLiked { get; set; }
    }
}
