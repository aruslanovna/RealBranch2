using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Topic
    {
        public Topic()
        {
            Articles = new HashSet<Article>();
        }
        [Key]
        public int TopicId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<News> News { get; set; }
        
        public virtual ICollection<Article> Articles { get; set; }

    }
}