using Newtonsoft.Json;

namespace OverheidioApi.NET.Models
{
    public class Link
    {
        [JsonProperty("href")]
        public string Url { get; set; }
    }
}