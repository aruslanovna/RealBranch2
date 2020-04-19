using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Species
    {
        [Key]
        public int SpeciestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
    }
}