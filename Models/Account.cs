using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Account")]
    public class Account
    {
        [Key, Required]
        public string NIK { get; set; }
        [Required]
        public string Password { get; set; }
        public Employee Employee { get; set; }
        public Profiling Profiling { get; set; }
    }
}
