using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Request_Module;
using Shared;
using Shared.DTOS.RequestDTOS;

namespace DomainLayer.Contracts
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        Task<Request> UpdateRequestAsync(UpdateRequestDTO request);
        public Task<List<Request>> GetAllWithIncludeData();
        Task<Request?> GetByIdWithReletadData(int? requestId);
        IQueryable<Request> GetAvailableRequestsForNurseAsync();
        IQueryable<Request> GetAvailableRequestsForDriverAsync();
        public Task<Request?> UpdateRequestStatuAsync(int requestId, RequestStatus status);
        public Task<Request?> AddDriverToRequestAsync(int requestId,int driverId);
        public Task<Request?> AddNurseToRequestAsync(int requestId,int nurseId);
        public Task<bool> UpdatePatientConfirmed(int requestId);
        Task<bool> IsDriverDoubleBookedAsync(int driverId, DateTime scheduledDate);
        Task<bool> IsNurseDoubleBookedAsync(int nurseId, DateTime scheduledDate);
        Task<bool> IsAmbulanceDoubleBookedAsync(int ambulanceId, DateTime scheduledDate);
    }
}
