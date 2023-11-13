using System.ComponentModel.DataAnnotations;

namespace JwtAuthExample.WrapperModels.UserModels
{
    public class UserRegisterModel
    {
        [Phone(ErrorMessage = "Invalid contact")]
        [Required(ErrorMessage = "Contact is required")]
        public string Contact { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Password must be atleast six digit long")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string ProfilePicture { get; set; }
    }
}
