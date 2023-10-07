using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Tweet
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User user { get; set; }
        public string message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
