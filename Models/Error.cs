using Newtonsoft.Json;
using System.Collections.Generic;

namespace helping_hand.Models
{
    public class Error
    {
        [JsonProperty("status")]
        public int? Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("errors")]
        public IDictionary<string, string[]> Errors { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
