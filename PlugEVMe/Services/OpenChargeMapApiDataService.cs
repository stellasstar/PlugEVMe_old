using System;
using System.Collections.Generic;
using PlugEVMe.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using PlugEVMe.Generators;
using System.Diagnostics;

namespace PlugEVMe.Services
{
	public class OpenChargeMapApiDataService : IDisposable
	{
		//private LogGenerator logger = new LogGenerator("OpenChargeMapLog.txt");

		readonly Uri _baseUri;
		readonly Uri mappingServiceUri = new Uri("https://api.openchargemap.io");
		readonly string key = "80797cf5-10fb-435a-97c1-25f8489dc2f3";
		readonly IDictionary<string, string> _headers;

		readonly string appClient = "PlugEVMe";
		readonly string output = "application/json";
		readonly string maxresults = "10";
		readonly string verbose = "true";
		readonly string distance = "10";
		readonly string distanceUnit = "KM";
		readonly HttpMethod method = HttpMethod.Get; // Default to GET
		private static readonly HttpClient _client = new HttpClient();                                   // Create an HttpClient and set the timeout for requests


		object requestData = null;
		private string _url;

		public string generatedurl
		{
			get { return _url; }
			set
			{
				_url = value;
			}
		}

		public OpenChargeMapApiDataService(double lat, double lon)
		{
			_baseUri = new Uri(mappingServiceUri, "/v3/poi/");
			string url = "?output=json";
			_headers = new Dictionary<string, string>();
			String latitude = Convert.ToString(lat);
			String longitude = Convert.ToString(lon);
			String polyline = @"aoagI|iaN?ir}Ahr}A??hr}Air}A?";


			//  adding to the url line to get results

			//_headers.Add("User-Agent", appClient);
			//_headers.Add("X-API-Key", key);
			_headers.Add("latitude", latitude);
			_headers.Add("longitude", longitude);
			_headers.Add("distance", distance);
			_headers.Add("distanceunit", distanceUnit);
			_headers.Add("maxresults", maxresults);
			_headers.Add("verbose", verbose);
			//_headers.Add("polyline", polyline);

			// embedding in the client request header, key and app name
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue(output));
			_client.DefaultRequestHeaders.Add("User-Agent", appClient);
			_client.DefaultRequestHeaders.Add("X-API-Key", key);

			// Add headers to request
			if (_headers != null)
			{
				foreach (var h in _headers)
				{
					//_client.DefaultRequestHeaders.Add(h.Key, h.Value);
					url += "&" + h.Key + "=" + h.Value;
				}
			}
			this.generatedurl = url;
		}

		public async Task<String> GetEntriesAsync()
		{
			//LogGenerator _logger = this.logger;
			string data = null;
			HttpResponseMessage _responseMessage = null;

			Uri _complete = new Uri(_baseUri, this.generatedurl);
			Debug.WriteLine("Uri REQUEST MESSAGE: " + _complete.ToString());
			Debug.WriteLine("Method REQUEST MESSAGE: " + method.ToString());

			try
			{
				//HttpRequestMessage _request = new HttpRequestMessage();
				//_request.RequestUri = new Uri(_baseUri, this.generatedurl);
				//ttpRequestMessage _request = new HttpRequestMessage(method, _complete);
				var continuation = Task.Run(async () => {
					_responseMessage = await _client.GetAsync(_complete).ConfigureAwait(true);
					_responseMessage.EnsureSuccessStatusCode();
				});
				continuation.Wait();
				// Send the request to the openchargemap server

				Debug.WriteLine("ResponseMessage Code: " + _responseMessage.IsSuccessStatusCode.ToString());

				if (_responseMessage.IsSuccessStatusCode)
				{
					data = await _responseMessage.Content.ReadAsStringAsync().ConfigureAwait(true);
					Debug.WriteLine(JToken.Parse(data).ToString(Formatting.Indented));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("EXCEPTION, couldn't retrieve httpclient: " + ex);
			}
			finally
			{
				// Dispose of HttpResponseMessage to ensure we free up system resources 
				// and release the network connection (if that hasn't already happened).
				// Not required in a majority of common cases but always safe to do as long
				// as you have not passed the content onto other threads / consumers.
			//	_responseMessage.Dispose();
			//	_client.Dispose();
			}

			return JToken.Parse(data).ToString(Formatting.Indented) ?? String.Empty;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		//      public async Task<PlugEVMeEntry> GetEntryAsync(string id)
		//      {
		//          var url = new Uri(_baseUri, string.Format("/tables/entry/{0}", id));
		//          var response = await SendRequestAsync<PlugEVMeEntry>(url, HttpMethod.Get, _headers);

		//          return response;
		//      }

		//      public async Task<PlugEVMeEntry> AddEntryAsync(PlugEVMeEntry entry)
		//{
		//	var url = new Uri(_baseUri, "/tables/entry");
		//	var response = await SendRequestAsync<PlugEVMeEntry>(url, HttpMethod.Post, _headers, entry);

		//	return response;
		//}

		//public async Task<PlugEVMeEntry> UpdateEntryAsync(PlugEVMeEntry entry)
		//{
		//	var url = new Uri(_baseUri,	string.Format("/tables/entry/{0}", entry.Id));
		//	var response = await SendRequestAsync<PlugEVMeEntry>(url, new HttpMethod("PATCH"), _headers, entry);

		//	return response;
		//}

		//public async Task RemoveEntryAsync(PlugEVMeEntry entry)
		//{
		//	var url = new Uri(_baseUri,	string.Format("/tables/entry/{0}", entry.Id));
		//	var response = await SendRequestAsync<PlugEVMeEntry>(url, HttpMethod.Delete, _headers);
		//}
	}
}