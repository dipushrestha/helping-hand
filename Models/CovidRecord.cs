using System;

namespace helping_hand.Models
{
    public class CovidRecord
    {
        public int Cases { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public int Active { get; set; }
        public int TodayCases { get; set; }
        public int TodayDeaths { get; set; }
        public int TodayRecovered { get; set; }
        public int TodayActive { get; set; }
    }
}
