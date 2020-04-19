﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
  public  class Article
    {
        [Key]
        public int ArticleId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Content),
                  ErrorMessageResourceName = "TitleRequired")]
        [StringLength(30, ErrorMessage = "Max length is 30 symbols")]
        public string Name { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Content),
                ErrorMessageResourceName = "BodyRequired")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Content),
                ErrorMessageResourceName = "BrieflyRequired")]
        public string Briefly { get; set; }
        public DateTime? PostDate { get; set; }
        public byte[] Photo { get; set; }
        public int? TopicId { get; set; }
    
     
        public virtual ICollection<Comment> Comments { get; set; }
      
        public virtual Topic Topics { get; set; }
       
    }
}
