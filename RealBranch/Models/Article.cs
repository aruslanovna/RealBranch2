//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealBranch.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string Briefly { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public byte[] Photo { get; set; }
        public Nullable<int> TopicId { get; set; }
        public Nullable<int> CategoryId { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}