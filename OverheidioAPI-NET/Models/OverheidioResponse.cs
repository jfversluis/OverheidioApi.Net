using Newtonsoft.Json;

namespace OverheidioApi.NET.Models
{
    public class OverheidioResponse
    {
        public int TotalItemCount { get; set; }
        public int PageCount { get; set; }
        public int Size { get; set; }

        [JsonProperty("_links")]
        public HalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        public Results Results { get; set; }
    }

    public class Results
    {
        [JsonProperty("rechtspersoon")]
        public Corporation[] Corporations { get; set; }

        [JsonProperty("kenteken")]
        public Vehicle[] Vehicles { get; set; }
    }

    public class HalLinks
    {
        [JsonProperty("self")]
        public Link EntityLink { get; set; }

        [JsonProperty("next")]
        public Link NextPageLink { get; set; }

        [JsonProperty("first")]
        public Link FirstPageLink { get; set; }

        [JsonProperty("last")]
        public Link LastPageLink { get; set; }
    }
}