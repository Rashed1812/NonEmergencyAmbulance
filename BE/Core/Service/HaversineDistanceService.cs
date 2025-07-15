using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAbstraction;

namespace Service
{
    public class HaversineDistanceService : IDistanceService
    {
        private readonly IGeocodingService _geocodingService;

        public HaversineDistanceService(IGeocodingService geocodingService)
        {
            _geocodingService = geocodingService;
        }

        public async Task<double> CalculateKMAsync(string fromAddress, string toAddress)
        {
            var from = await _geocodingService.GetCoordinatesAsync(fromAddress);
            var to = await _geocodingService.GetCoordinatesAsync(toAddress);

            if (from == null || to == null)
                return 0;

            return GeoUtils.CalculateDistance(from.Value.Lat, from.Value.Lng, to.Value.Lat, to.Value.Lng);
        }
    }
}
