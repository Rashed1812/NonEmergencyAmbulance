using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Request_Module;

namespace DomainLayer.Contracts
{
    public interface ITripPriceCalculator
    {
        public decimal Calculate(Request request, double distanceKM);
    }
}
