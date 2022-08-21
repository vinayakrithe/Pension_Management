using System.ComponentModel.DataAnnotations;

namespace AuthenticationMicroservice.Models
{
    public class RegistrationModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}", ErrorMessage = "Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters")]
        public string Password { get; set; }
    }
}
