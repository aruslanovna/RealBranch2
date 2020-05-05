﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.LINQmodels
{
   public class UserProfileModel
    {

       

        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public string Briefly { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public int? TopicId { get; set; }
        public string UserName { get; set; }
        public List<string> Comment {get; set;}
        public List<string> CommentedUserName { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DatePost { get; set; }
        [DataType(DataType.Date)]
        public List<DateTime?> CommentDate { get; set; }
        public IEnumerable<Note> Notes { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public int CommentId { get; set; }
        public List<string> CommentedUserId { get; set; }

    }
}
