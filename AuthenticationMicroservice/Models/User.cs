using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationMicroservice.Models
{
    public class User
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Username should be minimum of 3 characters and Maximum of 15 characters")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}", ErrorMessage = "Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters")]
        public string Password { get; set; }

        [NotMapped]
        public string Token { get; set; }

        [NotMapped]
        public DateTime ExpirationDate { get; set; }
    }
}
