using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Follower
    {
        [Key]
      public  int Id { get; set; }
        [Required]
        public  string UserId { get; set; }
        [Required]
        public string FollowerId { get; set; }
    }
}
