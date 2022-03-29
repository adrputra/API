using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class AccountRole
    {
        [Key, Required]
        public int ID { get; set; }
        [Required]
        public string NIK { get; set; }
        [Required]
        public int RoleID { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
