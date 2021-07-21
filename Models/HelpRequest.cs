using System;
using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models
{
    public class HelpRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Requested Date field is required.")]
        public DateTime RequestedDate { get; set; }

        public DateTime? HelpedDate { get; set; }

        [Required(ErrorMessage = "Urgency level is required.")]
        public string Urgency { get; set; }

        [Required(ErrorMessage = "Services is required.")]
        public string Services { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Not a valid phone number.")]
        public string PhoneNumber { get; set; }

        public string Message { get; set; }
    }
}
