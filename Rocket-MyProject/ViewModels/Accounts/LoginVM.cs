using System.ComponentModel.DataAnnotations;

namespace Rocket_MyProject.ViewModels.Accounts
{
    public class LoginVM
    {
        [Required, MinLength(3)]
        public string UsernameOfEmail { get; set; }

        [Required, DataType(DataType.Password)]

        public string Password { get; set; }
        public bool RememberMe { get; set; }    
    }
}
