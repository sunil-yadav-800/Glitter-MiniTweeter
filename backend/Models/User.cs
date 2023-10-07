using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string profilePic { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Follower> followerrr { get; set; }
        public virtual ICollection<Follower> followeeee { get; set; }
        public virtual ICollection<Tweet> tweets { get; set; }
        public virtual ICollection<Like> likes { get; set; }
    }
}
