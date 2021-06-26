using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models.Dto
{
    public class RegisterDto
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
