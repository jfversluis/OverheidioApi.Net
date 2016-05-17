using Newtonsoft.Json;

namespace OverheidioApi.NET.Models
{
    public class Corporation
    {
        [JsonProperty("actief")]
        public bool Active { get; set; }

        [JsonProperty("bestaandehandelsnaam")]
        public string ExistingTradename { get; set; }

        [JsonProperty("dossiernummer")]
        public string Dossiernumber { get; set; }

        [JsonProperty("handelsnaam")]
        public string Tradename { get; set; }

        [JsonProperty("huisnummer")]
        public string HouseNumber { get; set; }

        [JsonProperty("huisnummertoevoeging")]
        public string HouseNumberAddition { get; set; }

        [JsonProperty("plaats")]
        public string City { get; set; }

        [JsonProperty("postcode")]
        public string Zipcode { get; set; }

        [JsonProperty("straat")]
        public string Street { get; set; }

        [JsonProperty("subdossiernummer")]
        public string Subdossiernumber { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("vestigingsnummer")]
        public int EstablishmentNumber { get; set; }

        [JsonProperty("_links")]
        public HalLinks Links { get; set; }
    }
}