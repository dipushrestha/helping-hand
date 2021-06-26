using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace helping_hand.Models
{ 
    public class Request
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Urgency { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Address { get; set; }

        public string Summary { get; set; }

    }
}
