using Newtonsoft.Json;

namespace OverheidioApi.NET.Models
{
	public class SuggestResult
	{
		[JsonProperty("handelsnaam")]
		public TradenameSuggestion[] Tradenames { get; set; }

		[JsonProperty("straat")]
		public StreetSuggestion[] Streets { get; set; }

		[JsonProperty("dossiernummer")]
		public DossiernumberSuggestion[] Dossiernumbers { get; set; }
	}

	public class DossiernumberSuggestion
	{
		[JsonProperty("text")]
		public string Dossiernumber { get; set; }

		[JsonProperty("extra")]
		public ExtraData extra { get; set; }
	}

	public class StreetSuggestion
	{
		[JsonProperty("text")]
		public string Street { get; set; }

		[JsonProperty("extra")]
		public ExtraData extra { get; set; }
	}

	public class TradenameSuggestion
	{
		[JsonProperty("text")]
		public string Tradename { get; set; }

		public ExtraData extra { get; set; }
	}

	public class ExtraData
	{
		public string Id { get; set; }

		[JsonProperty("postcode")]
		public string Zipcode { get; set; }

		[JsonProperty("handelsnaam")]
		public string Tradename { get; set; }
	}
}