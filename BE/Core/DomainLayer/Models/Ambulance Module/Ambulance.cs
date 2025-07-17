using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Trip_Module;

namespace DomainLayer.Models.Ambulance_Module
{
    public class Ambulance
    {
        public int AmbulanceId { get; set; }
        public string PlateNumber { get; set; }
        public string CurrentLocation { get; set; }
        public AmbulanceStatus Status { get; set; }  
        public AmbulanceType Type { get; set; } = AmbulanceType.EquippedWithNurse;
        public ICollection<Trip> Trips { get; set; }
        public int? DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
