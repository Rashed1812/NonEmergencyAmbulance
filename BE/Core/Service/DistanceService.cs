using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAbstraction;

namespace Service
{
    public class DistanceService : IDistanceService
    {
        public Task<double> CalculateKMAsync(string fromAddress, string toAddress)
        {
            throw new NotImplementedException();
        }
    }
}
