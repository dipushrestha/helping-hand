using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
