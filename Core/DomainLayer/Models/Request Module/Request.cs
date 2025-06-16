using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Trip_Module;

namespace DomainLayer.Models.Request_Module
{
    public class Request
    {
        public int RequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public string PickupAddress { get; set; }
        public string DropOffAddress { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string EmergencyType { get; set; }
        public RequestStatus Status { get; set; }
        public string Notes { get; set; }
        public int AssignedAmbulanceId { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int? DriverId { get; set; }
        public Driver Driver { get; set; }
        public int? NurseId { get; set; }
        public Nurse Nurse { get; set; }
        public Trip Trip { get; set; }
    }
}
