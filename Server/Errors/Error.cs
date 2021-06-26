using Newtonsoft.Json;

namespace helping_hand.Server.Errors
{
    public class Error
    {
        [JsonProperty("statuscode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
