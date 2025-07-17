using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Request_Module;
using DomainLayer.Models.Trip_Module;

namespace DomainLayer.Contracts
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient?> GetPatientByUserIdAsync(string userId);
        Task<IEnumerable<Request>> GetRequestsByPatientIdAsync(int patientId);
        Task<IEnumerable<Trip>> GetTripsByPatientIdAsync(int patientId);
    }
}
