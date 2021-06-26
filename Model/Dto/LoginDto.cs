using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models.Dto
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
