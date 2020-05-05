using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Photo { get; set; }
        public string Status { get; set; }
        public string Experience { get; set; }
        public string WorkPlace { get; set; }
        public string Position { get; set; }
        public string Info { get; set; }
    }
}
