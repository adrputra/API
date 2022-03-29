using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("University")]
    public class University
    {
        [Key, Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Education> Educations{ get; set; }
    }
}
