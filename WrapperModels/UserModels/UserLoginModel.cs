using System.ComponentModel.DataAnnotations;

namespace JwtAuthExample.WrapperModels.UserModels
{
    public class UserLoginModel
    {
        [EmailAddress(ErrorMessage ="Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }


        [MinLength(6,ErrorMessage = "Password must be atleast six digit long")]
        [Required(ErrorMessage ="Password is required")]
        public string password { get; set; }
    }
}
