using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SelectedNews
    {
        [Key]
        public int SelectedNewsId { get; set; }
        public string UserId { get; set; }
      

        public int ArticleId { get; set; }

        public virtual Article Articles { get; set; }


    }
}
