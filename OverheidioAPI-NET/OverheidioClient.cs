using Newtonsoft.Json;
using OverheidioApi.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OverheidioApi.NET
{
	public class OverheidioClient
	{
		private const string ApiBaseUrl = "https://api.overheid.io/";
		private const string ApiBaseUrl2 = "https://overheid.io/";
		private readonly HttpClient _httpClient;

		/// <summary>
		/// Instantiates a new OverheidioClient
		/// </summary>
		/// <param name="apikey">API key which can be generated on overheid.io</param>
		public OverheidioClient(string apikey)
		{
			if (string.IsNullOrWhiteSpace(apikey))
				throw new ArgumentException("Parameter apikey needs a value");

			_httpClient = new HttpClient()
			{
				BaseAddress = new Uri(ApiBaseUrl)
			};

			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_httpClient.DefaultRequestHeaders.Add("ovio-api-key", apikey);
		}

		#region kvk
		/// <summary>
		/// Find corporations by specifying searchcriteria. Results will contain Dutch descriptions.
		/// Returned Corporation objects may not include all property values depending on the <paramref name="fields"/> parameter.
		/// </summary>
		/// <param name="size">How many results should be returned, default 100</param>
		/// <param name="page">Which page (if more results are available than specified <paramref name="size"/>) should be retrieved</param>
		/// <param name="order">How should the results be ordered, "asc" or "desc"</param>
		/// <param name="sort">On which field should be sorted</param>
		/// <param name="query">String to search for in <paramref name="queryfields"/>. * wildcard is available.</param>
		/// <param name="queryfields">Fields in which should be searched for <paramref name="query"/></param>
		/// <param name="fields">Fields to be included in the result. By default tradename, dossier- and subdossiernumber are included. See following link for all possible fields <see href="https://overheid.io/documentatie/kvk#show"/></param>
		/// <param name="filters">Used for specifying filters. Key is the field, value is the value to filter on. I.e. key: postcode, value: 3083cz. See following link for all possible fields <see href="https://overheid.io/documentatie/kvk#show"/></param>
		/// <returns>List of results found including JSON+HAL metadata</returns>
		public async Task<OverheidioResponse> FindCorporations(int size = 100, int page = 1, string order = "desc", string sort = "", string query = "",
			string[] queryfields = null, string[] fields = null, KeyValuePair<string, string>[] filters = null)
		{
			if (size <= 0)
				throw new ArgumentOutOfRangeException(nameof(size), "Parameter size cannot 0 or less");

			if (page <= 0)
				throw new ArgumentOutOfRangeException(nameof(page), "Parameter page cannot 0 or less");

			// TODO maybe use an enum?
			if (order.ToLowerInvariant() != "desc" && order.ToLowerInvariant() != "asc")
				throw new ArgumentOutOfRangeException(nameof(order), "Parameter order can only be asc or desc");

			var urlBuilder = new StringBuilder($"/openkvk?size={size}&page={page}&order={order}");

			if (!string.IsNullOrWhiteSpace(sort))
				urlBuilder.Append($"&sort={sort}");

			if (!string.IsNullOrWhiteSpace(query))
				urlBuilder.Append($"&query={query}");

			if (queryfields != null && queryfields.Length > 0)
				foreach (string field in queryfields)
					urlBuilder.Append($"&queryfields[]={field}");

			if (fields != null && fields.Length > 0)
				foreach (var field in fields)
					urlBuilder.Append($"&fields[]={field}");

			if (filters != null && filters.Length > 0)
				foreach (KeyValuePair<string, string> filter in filters)
					urlBuilder.Append($"&filters[{filter.Key}]={filter.Value}");

			var jsonResult = await _httpClient.GetStringAsync(urlBuilder.ToString());

			return JsonConvert.DeserializeObject<OverheidioResponse>(jsonResult);
		}

		/// <summary>
		/// Get a data about a specific corporation by dossier- and subdossiernumber
		/// </summary>
		/// <param name="dossierNumber">The chamber of commerce dossiernumber</param>
		/// <param name="subdossierNumber">The chamber of commerce subdossiernumber</param>
		/// <returns>Object with all information about the corporation, or null when a corporation isn't found or any other error occurs</returns>
		public async Task<Corporation> GetCorporation(string dossierNumber, string subdossierNumber)
		{
			if (string.IsNullOrWhiteSpace(dossierNumber))
				throw new ArgumentException("Parameter dossierNumber cannot be empty");

			if (string.IsNullOrWhiteSpace(subdossierNumber))
				throw new ArgumentException("Parameter subdossierNumber cannot be empty");

			var urlBuilder = new StringBuilder($"/openkvk/?filters[dossiernummer]={ dossierNumber}&filters[subdossiernummer]={ subdossierNumber}");

			var resultJson = await _httpClient.GetStringAsync(urlBuilder.ToString());

			try
			{
				OverheidioResponse resultCorporation = JsonConvert.DeserializeObject<OverheidioResponse>(resultJson);

				return resultCorporation.Results.Corporations[0];
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Get a data about a specific corporation by the id aquierd by the api
		/// </summary>
		/// <param name="id">The chamber of commerce dossier number</param>
		/// <returns>Object with all information about the corporation, or null when a corporation isn't found or any other error occurs</returns>
		public async Task<Corporation> GetCorporation(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentException("Parameter id cannot be empty");

			var resultJson = await _httpClient.GetStringAsync($"{id}");

			try
			{
				var resultCorporation = JsonConvert.DeserializeObject<Corporation>(resultJson);

				return resultCorporation;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Retrieves suggestions, useable in a autocomplete searchbox for example.
		/// </summary>
		/// <param name="query">String to search for, wildcards not supported searches as: searchte*, spaces are stripped</param>
		/// <param name="size">How many results are returned. Default 10, maximum 25</param>
		/// <param name="fields">Which fields to include in the suggestion. Can only be handelsnaam (tradename), straat (street) and dossiernummer (dossiernumber)</param>
		/// <returns>List of suggestions found with the given <paramref name="query"/> or null if an error occurs</returns>
		public async Task<SuggestResult> GetSuggestions(string query, int size = 10, string[] fields = null)
		{
			if (size <= 0)
				throw new ArgumentOutOfRangeException(nameof(size), "Parameter size cannot 0 or less");

			if (size > 25)
				throw new ArgumentOutOfRangeException(nameof(size), "Parameter size cannot be more than 25");

			if (fields != null && fields.Except(new[] { "handelsnaam", "straat", "dossiernummer" }).Any())
				throw new ArgumentOutOfRangeException(nameof(fields), "Parameter fields can only contain values: handelsnaam, straat and dossiernummer");

			var resultJson = await _httpClient.GetStringAsync($"suggest/kvk/{query}?size={size}");

			try
			{
				var resultSuggestions = JsonConvert.DeserializeObject<SuggestResult>(resultJson);

				return resultSuggestions;
			}
			catch
			{
				return null;
			}
		}
		#endregion

		#region Vehicleinfo
		/// <summary>
		/// Find vehicles by specifying searchcriteria. Results will contains Dutch descriptions.
		/// Returned Vehicle objects may not include all property values depending on the <paramref name="fields"/> parameter.
		/// </summary>
		/// <param name="size">How many results should be returned, default 100</param>
		/// <param name="page">Which page (if more results are available than specified <paramref name="size"/>) should be retrieved</param>
		/// <param name="order">How should the results be ordered, "asc" or "desc"</param>
		/// <param name="sort">On which field should be sorted</param>
		/// <param name="query">String to search for in <paramref name="queryfields"/>. * wildcard is available.</param>
		/// <param name="queryfields">Fields in which should be searched for <paramref name="query"/></param>
		/// <param name="fields">Fields to be included in the result. By default date of first allowance, licenseplate, commercial designation, brand, and vehicle type are included. See following link for all possible fields <see href="https://overheid.io/documentatie/voertuiggegevens#show"/></param>
		/// <param name="filters">Used for specifying filters. Key is the field, value is the value to filter on. I.e. key: kenteken, value: 02-JZX-1. See following link for all possible fields <see href="https://overheid.io/documentatie/voertuiggegevens#show"/></param>
		/// <returns>List of results found including JSON+HAL metadata</returns>
		public async Task<OverheidioResponse> FindVehicles(int size = 100, int page = 1, string order = "desc", string sort = "", string query = "",
			string[] queryfields = null, string[] fields = null, KeyValuePair<string, string>[] filters = null)
		{
			if (size <= 0)
				throw new ArgumentOutOfRangeException(nameof(size), "Parameter size cannot 0 or less");

			if (page <= 0)
				throw new ArgumentOutOfRangeException(nameof(page), "Parameter page cannot 0 or less");

			// TODO maybe use an enum?
			if (order.ToLowerInvariant() != "desc" && order.ToLowerInvariant() != "asc")
				throw new ArgumentOutOfRangeException(nameof(order), "Parameter order can only be asc or desc");

			_httpClient.BaseAddress = new Uri(ApiBaseUrl2);

			var urlBuilder = new StringBuilder($"/api/voertuiggegevens?size={size}&page={page}&order={order}");

			if (!string.IsNullOrWhiteSpace(sort))
				urlBuilder.Append($"&sort={sort}");

			if (!string.IsNullOrWhiteSpace(query))
				urlBuilder.Append($"&query={query}");

			if (queryfields != null && queryfields.Length > 0)
				foreach (var field in queryfields)
					urlBuilder.Append($"&queryfields[]={field}");

			if (fields != null && fields.Length > 0)
				foreach (var field in fields)
					urlBuilder.Append($"&fields[]={field}");

			if (filters != null && filters.Length > 0)
				foreach (var filter in filters)
					urlBuilder.Append($"&filters[{filter.Key}]={filter.Value}");

			var jsonResult = await _httpClient.GetStringAsync(urlBuilder.ToString());

			return JsonConvert.DeserializeObject<OverheidioResponse>(jsonResult);
		}

		[Obsolete("fixed typo GetVerhicle")]
		public async Task<Vehicle> GetVerhicle(string licensePlate)
		{
			return await GetVehicle(licensePlate);
		}
		/// <summary>
		/// Retrieves details about the vehicle corresponding to the given <paramref name="licensePlate"/>
		/// </summary>
		/// <param name="licensePlate">The licenseplate in Dutch formatting i.e. 02-JZX-1</param>
		/// <returns>A Vehicle object containing the details</returns>
		public async Task<Vehicle> GetVehicle(string licensePlate)
		{
			_httpClient.BaseAddress = new Uri(ApiBaseUrl2);

			var resultJson = await _httpClient.GetStringAsync($"api/voertuiggegevens/{licensePlate}");

			var vehicleResult = JsonConvert.DeserializeObject<Vehicle>(resultJson);

			return vehicleResult;
		}
		#endregion

		#region BAG

		public async Task<OverheidioResponse> FindAdresses(int size = 100, int page = 1, string order = "desc", string sort = "", string query = "",
			string[] queryfields = null, string[] fields = null, KeyValuePair<string, string>[] filters = null)
		{
			if (size <= 0)
				throw new ArgumentOutOfRangeException(nameof(size), "Parameter size cannot 0 or less");

			if (page <= 0)
				throw new ArgumentOutOfRangeException(nameof(page), "Parameter page cannot 0 or less");

			// TODO maybe use an enum?
			if (order.ToLowerInvariant() != "desc" && order.ToLowerInvariant() != "asc")
				throw new ArgumentOutOfRangeException(nameof(order), "Parameter order can only be asc or desc");

			_httpClient.BaseAddress = new Uri(ApiBaseUrl2);

			var urlBuilder = new StringBuilder($"/api/bag?size={size}&page={page}&order={order}");

			if (!string.IsNullOrWhiteSpace(sort))
				urlBuilder.Append($"&sort={sort}");

			if (!string.IsNullOrWhiteSpace(query))
				urlBuilder.Append($"&query={query}");

			if (queryfields != null && queryfields.Length > 0)
				foreach (var field in queryfields)
					urlBuilder.Append($"&queryfields[]={field}");

			if (fields != null && fields.Length > 0)
				foreach (var field in fields)
					urlBuilder.Append($"&fields[]={field}");

			if (filters != null && filters.Length > 0)
				foreach (var filter in filters)
					urlBuilder.Append($"&filters[{filter.Key}]={filter.Value}");


			var jsonResult = await _httpClient.GetStringAsync(urlBuilder.ToString());

			return JsonConvert.DeserializeObject<OverheidioResponse>(jsonResult);
		}

		#endregion
	}
}