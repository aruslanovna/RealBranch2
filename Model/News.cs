using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
        public int TopicId { get; set; }

        public virtual Topic Topics { get; set; }


    }
}