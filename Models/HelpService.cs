using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models
{
    public class HelpService
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Service name is required.")]
        public string Service { get; set; }

        //[Required]
        public bool HasQuantity { get; set; }

        [Required(ErrorMessage = "Active status is required.")]
        public bool IsActive { get; set; }

    }
}
