using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models
{
    public class Urgency
    {
        public int Id { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
