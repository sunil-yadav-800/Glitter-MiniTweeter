using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User user { get; set; }
        public int TweetId { get; set; }
        public virtual Tweet tweet { get; set; }
    }
}
