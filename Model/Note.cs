using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        [Required]
        public string Name { get; set; }
       
        public string Description { get; set; }
        public bool Check { get; set; }
        public string UserId { get; set; }
        [Required]
        public byte[] Photo { get; set; }
    }
}
