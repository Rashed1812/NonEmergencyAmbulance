using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.TripDTOs
{
    public class TripDTO
    {
        public int TripId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PickupAddress { get; set; }
        public string DropOffAddress { get; set; }
        public string DriverName { get; set; }
        public string NurseName { get; set; }
        public double DistanceKM { get; set; }
        public decimal Price { get; set; }
        public TripStatus TripStatus { get; set; }
    }
}
