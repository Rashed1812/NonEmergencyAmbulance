using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.Request_Module;

namespace Persistence.Repositories
{
    public class TripPriceCalculator : ITripPriceCalculator
    {
        public decimal Calculate(Request request, double distanceKM)
        {
            decimal basePrice = 50;
            decimal perKmRate = 7;
            return basePrice + (decimal)distanceKM * perKmRate;
        }
    }
}
