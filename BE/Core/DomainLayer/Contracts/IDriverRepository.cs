using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Identity_Module;

namespace DomainLayer.Contracts
{
    public interface IDriverRepository :IGenericRepository<Driver>
    {
        Task<IEnumerable<Driver>> GetAllWithRelatedData();
        Task<Driver> GetByIdWithRelatedData(int id);
        Task<Driver> GetDriverByIdWithAmbulance(int id);
        Task<Driver> GetByIdWithRequestsAndTripsAsync(int driverId);
    }
}
