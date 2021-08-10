using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace helping_hand.Models
{
    public class Notice
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        [Url]
        public string Link { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApiUser User { get; set; }
    }
}
