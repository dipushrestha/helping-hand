using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models
{
    public class HelpService
    {
        public int Id { get; set; }

        [Required]
        public string Service { get; set; }

        //[Required]
        public bool HasQuantity { get; set; }

    }
}
