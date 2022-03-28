using System.ComponentModel.DataAnnotations;

namespace API.ViewModel
{
    public class ForgotPasswordVM
    {
        [Required]
        public string Email { get; set; }
    }
}
