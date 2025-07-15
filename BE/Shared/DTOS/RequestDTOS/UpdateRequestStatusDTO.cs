using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.RequestDTOS
{
    public class UpdateRequestStatusDTO
    {
        public int RequestId { get; set; }
        public RequestStatus Status { get; set; }
    }
}
