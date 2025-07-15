using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.TripDTOs
{
    public enum TripStatus
    {
        Pending = 0,
        Assigned = 1,
        Ongoing = 2,       
        Completed = 3,
        Cancelled = 4,
        Failed = 5
    }
}
