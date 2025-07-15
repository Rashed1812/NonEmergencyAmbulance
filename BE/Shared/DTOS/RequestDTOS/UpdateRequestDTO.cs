using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.RequestDTOS
{
    public class UpdateRequestDTO
    {
        public int RequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public string PickupAddress { get; set; }
        public string DropOffAddress { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string EmergencyType { get; set; }
    }
}
