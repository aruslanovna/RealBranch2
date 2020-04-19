using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Plant
    {
        [Key]
        public int PlantId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public int? SpeciestId { get; set; }

        //public virtual Species Species { get; set; }
        public byte[] Photo { get; set; }
        public int NumberOfSorts { get; set; }
        public string Kind { get; set; }


    }
}