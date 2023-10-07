using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Follower
    {
        [Key]
        public int Id { get; set; }
        public int FollowerId { get; set; }
        
        public virtual User follower { get; set; }
        public int FolloweeId { get; set; }
        
        public virtual User followee { get; set; }
    }
}
