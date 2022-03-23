using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Education")]
    public class Education
    {
        [Key, Required]
        public int ID { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }
        [Required]
        public int UniversityId { get; set; }
        public ICollection<Profiling> Profilings { get; set; }
        public University University { get; set; }
    }
}
