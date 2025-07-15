using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Ambulance_Module;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Request_Module;
using Shared.DTOS.TripDTOs;

namespace DomainLayer.Models.Trip_Module
{
    public class Trip
    {
        public int TripId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double DistanceKM { get; set; }
        public TripStatus TripStatus { get; set; }
        public decimal Price { get; set; }
        public int AmbulanceId { get; set; }
        public Ambulance Ambulance { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int? NurseId { get; set; }
        public Nurse Nurse { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}
