using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Directory
    {
        [Key]
        public int DirectoryId { get; set; }
        public string UserId { get; set; }
        public int PlantId { get; set; }

        public Plant Plants { get; set; }

    }
}
