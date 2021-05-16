using System;
using System.Collections.Generic;
using System.Text;

namespace helping_hand.Shared
{
    public class Request
    {
        public DateTime Date { get; set; }

        public string Urgency { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Address { get; set; }

        public string Summary { get; set; }

    }
}
