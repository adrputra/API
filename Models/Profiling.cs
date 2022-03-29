using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Profiling")]
    public class Profiling
    {
        [Key, Required]
        public string NIK { get; set; }
        [Required]
        public int EducationId { get; set; }
        public virtual Account Account { get; set; }
        public virtual Education Education { get; set; }
    }
}
