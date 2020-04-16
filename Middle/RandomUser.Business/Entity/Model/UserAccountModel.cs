using System.ComponentModel.DataAnnotations;

namespace RandomUser.Business.Entity.Model
{
    public class UserAccountModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
