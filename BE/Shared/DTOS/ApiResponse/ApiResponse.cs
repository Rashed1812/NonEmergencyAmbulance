using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.ApiResponse
{
    public class ApiResponse
    {
        public bool IsSuccessful { get; set; }
        public dynamic Data { get; set; }
    }

}
