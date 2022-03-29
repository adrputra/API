using System.ComponentModel.DataAnnotations;

namespace API.ViewModel
{
    public class EmailVM
    {
        [Required]
        public string Email { get; set; }
    }
}
