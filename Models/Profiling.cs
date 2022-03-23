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
        public int Education_id { get; set; }
        public Account Account { get; set; }
        public Education Education { get; set; }
    }
}
