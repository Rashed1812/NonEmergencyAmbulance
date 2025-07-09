using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum RequestStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2,
        InProgress = 3,
        Completed = 4,
        Cancelled = 5
    }
}
