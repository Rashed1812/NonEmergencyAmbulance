using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServiceAbstraction;

namespace Service
{
    public class NominatimGeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient = new();
        public async Task<(double Lat, double Lng)?> GetCoordinatesAsync(string address)
        {
            var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&limit=1";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(content);
            if (json.Count == 0)
                return null;

            double lat = double.Parse((string)json[0].lat, CultureInfo.InvariantCulture);
            double lon = double.Parse((string)json[0].lon, CultureInfo.InvariantCulture);
            return (lat, lon);
        }
    }
}
