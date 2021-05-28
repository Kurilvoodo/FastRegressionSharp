using System.ComponentModel.DataAnnotations;

namespace FRSWebApp.Models.Login
{
    public class SignInLoginVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}