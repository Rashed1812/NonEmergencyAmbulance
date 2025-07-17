using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.RequestDTOS
{
    public class RequestDTO
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
        public string? PatientName { get; set; }
        public string? PatientPhone { get; set; }
        public string? PatientAddress { get; set; }
        public string? PatientImageUrl { get; set; }
        public int? DriverId { get; set; }
        public string? DriverName { get; set; }
        public string? DriverPhone { get; set; }
        public string? AmbulancePlateNumber { get; set; }
        public string? AmbulanceType { get; set; }

        // Nurse Info
        public int? NurseId { get; set; }
        public string? NurseName { get; set; }
        public string? NursePhone { get; set; }
    }
}
