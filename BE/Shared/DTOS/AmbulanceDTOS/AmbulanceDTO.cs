using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Ambulance_Module;

namespace Shared.DTOS.AmbulanceDTOS
{
    public class AmbulanceDTO
    {
        public int AmbulanceId { get; set; }
        public string PlateNumber { get; set; }
        public string CurrentLocation { get; set; }
        public AmbulanceStatus Status { get; set; }
        public AmbulanceType Type { get; set; }
        public int? DriverId { get; set; }
        public string DriverName { get; set; }
    }
} 