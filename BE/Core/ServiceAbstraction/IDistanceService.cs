using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IDistanceService
    {
        Task<double> CalculateKMAsync(string fromAddress, string toAddress);

    }
}
