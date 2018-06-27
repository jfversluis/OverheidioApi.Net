using System.Collections.Generic;
using Newtonsoft.Json;

namespace OverheidioApi.NET.Models
{
	public class Corporation
	{
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

		[JsonProperty("_links")]
		public HalLinks Links { get; set; }

		[JsonProperty("actief")]
		public bool? Active { get; set; }

		[JsonProperty("bestaandehandelsnaam")]
		public IList<string> ExistingTradename { get; set; }

		[JsonProperty("statutairehandelsnaam")]
		public IList<string> RegisteredTradename { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("vestigingsnummer")]
		public int EstablishmentNumber { get; set; }

		[JsonProperty("locatie")]
		public Location Location { get; set; }

		[JsonProperty("vbo_id")]
		public string Vbo_id { get; set; }

		[JsonProperty("pand_id")]
		public string Pand_id { get; set; }

		[JsonProperty("sbi")]
		public IList<string> SBI { get; set; }

		[JsonProperty("omschrijving")]
		public string Description { get; set; }

		[JsonProperty("RSIN")]
		public string RSIN { get; set; }

		[JsonProperty("BTW")]
		public string BTW { get; set; }

		[JsonProperty("websites")]
		public IList<string> Websites { get; set; }
	}
}