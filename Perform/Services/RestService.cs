using System.Diagnostics;
using System.Text.Json;

namespace Perform.Services
{
    public class RestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public List<string[]> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<string[]>> RefreshDataAsync()
        {
            Items = new List<string[]>();

            Uri uri = new Uri("http://gperform.azurewebsites.net/output.csv"); // replace with your Blazor server's URL
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.ConnectionClose = true; // This disables Keep-Alive

                HttpResponseMessage response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = content.Split('\n').Select(line => line.Split(',')).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

    }

}
