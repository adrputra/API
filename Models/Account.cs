using System;
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
        public int OTP { get; set; }
        public DateTime ExpiredToken{ get; set; }
        public bool isUsed { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Profiling Profiling { get; set; }
    }
}
