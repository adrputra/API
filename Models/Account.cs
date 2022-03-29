using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
