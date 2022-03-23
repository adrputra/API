using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("EMPLOYEE")]
    public class Employee
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public Account Account { get; set; }
    }
    public enum Gender { Male, Female }
}
