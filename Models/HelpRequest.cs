using System;
using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models
{
    public class HelpRequest
    {
        public int Id { get; set; }

        [Required]
        public DateTime RequestedDate { get; set; }

        public DateTime? HelpedDate { get; set; }

        [Required]
        public string Urgency { get; set; }

        [Required]
        public string Services { get; set; }

        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Message { get; set; }
    }
}
