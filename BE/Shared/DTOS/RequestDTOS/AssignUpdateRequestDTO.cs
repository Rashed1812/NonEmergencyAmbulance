using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.RequestDTOS
{
    public class AssignUpdateRequestDTO
    {
        public int RequestId { get; set; }
        public RequestStatus Status { get; set; }
        public int? DriverId { get; set; }
        public int? NurseId { get; set; }
        public string? PatientName { get; set; }
        public string? PatientPhone { get; set; }
    }
}
