using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models
{
    public class Notice
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }
    }
}
