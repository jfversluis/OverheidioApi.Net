using Newtonsoft.Json;

namespace OverheidioApi.NET.Models
{
	public class Address
	{
		[JsonProperty("gemeentenaam")]
		public string Municipality { get; set; }

		[JsonProperty("huisletter")]
		public string HouseLetter { get; set; }

		[JsonProperty("huisnummer")]
		public string HouseNumber { get; set; }

		[JsonProperty("huisnummertoevoeging")]
		public string HouseNumberAddition { get; set; }

		[JsonProperty("locatie")]
		public Location location { get; set; }

		[JsonProperty("openbareruimtenaam")]
		public string StreetName { get; set; }

		[JsonProperty("postcode")]
		public string Postcode { get; set; }

		[JsonProperty("provincienaam")]
		public string ProvinceName { get; set; }

		[JsonProperty("typeadresseerbaarobject")]
		public string AdressableObjectType { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("woonplaatsnaam")]
		public string CityName { get; set; }

		[JsonProperty("_links")]
		public HalLinks Links { get; set; }
	}
}