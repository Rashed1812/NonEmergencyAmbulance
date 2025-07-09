using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.RequestDTOS
{
    public class CreateRequestDTO
    {
        public string PickupAddress { get; set; }
        public string DropOffAddress { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string EmergencyType { get; set; }
        public string Notes { get; set; }
        public int AssignedAmbulanceId { get; set; }
    }
}
