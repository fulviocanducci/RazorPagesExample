using System.ComponentModel.DataAnnotations;

namespace WebApplication22.Models
{
    public class LoginViewModel
    {
        [Required()]
        [EmailAddress()]
        public string Email { get; set; }

        [Required()]        
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
