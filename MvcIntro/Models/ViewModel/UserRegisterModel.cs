using MvcIntro.Validators;
using System.ComponentModel.DataAnnotations;

namespace MvcIntro.Models.ViewModel
{
    public class UserRegisterModel
    {
        [Required]
        [Display(Name= "Name", ResourceType = typeof(Resources.Models.UserRegister))]
        public string Name { get; set; }

        [Required]
        [StrongPassword]
        [Display(Name = "Password", ResourceType = typeof(Resources.Models.UserRegister))]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "PasswordRepeat", ResourceType = typeof(Resources.Models.UserRegister))]
        public string PasswordRepeat { get; set; }
    }
}