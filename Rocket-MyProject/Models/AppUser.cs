using Microsoft.AspNetCore.Identity;

namespace Rocket_MyProject.Models
{
    public class AppUser:IdentityUser
    {
        public string Name {  get; set; }
        public string Surname { get; set; }
    }
}
