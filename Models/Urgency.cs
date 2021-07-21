using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models
{
    public class Urgency
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Label is required.")]
        public string Label { get; set; }

        [Required(ErrorMessage = "Level is required.")]
        public int Level { get; set; }

        [Required(ErrorMessage = "Active status is required.")]
        public bool IsActive { get; set; }
    }
}
