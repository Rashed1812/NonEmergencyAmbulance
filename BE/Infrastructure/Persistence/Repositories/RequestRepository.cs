using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.Request_Module;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared;
using Shared.DTOS.RequestDTOS;

namespace Persistence.Repositories
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Request>> GetAllWithIncludeData()
        {
            var requestData = await _dbContext.Requests
                .Include(r => r.Patient)
                    .ThenInclude(d => d.User)
                .Include(r=> r.Nurse)
                    .ThenInclude(p => p.User)
                .Include(r => r.Driver)
                    .ThenInclude(p => p.User)
                .ToListAsync();
            return requestData;
        }

        #region Get Requests Pending To Make Nurse And driver choose Request
        public IQueryable<Request> GetAvailableRequestsForDriverAsync()
        {
            var requestData = _dbContext.Requests
                .Where(r => r.Status == RequestStatus.Pending && r.DriverId == null)
                .Include(r => r.Patient)
                .ThenInclude(p => p.User);

            return requestData;
        }

        public IQueryable<Request> GetAvailableRequestsForNurseAsync()
        {
            var requestData = _dbContext.Requests
                    .Where(r => r.Status == RequestStatus.Pending && r.NurseId == null)
                    .Include(r => r.Patient)
                    .ThenInclude(p => p.User);
            return requestData;
        }
        #endregion

        #region Update Request To Assign Driver And Nurse
        public async Task<Request?> AddDriverToRequestAsync(int requestId, int driverId)
        {
            var rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(
                "UPDATE Requests SET DriverId = {0} WHERE RequestId = {1} AND DriverId IS NULL",
                driverId, requestId);

            if (rowsAffected == 0)
                return null;

            return await _dbContext.Requests
                .Include(r => r.Patient)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);
        }

        public async Task<Request?> AddNurseToRequestAsync(int requestId, int nurseId)
        {
            var rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(
                "UPDATE Requests SET NurseId = {0} WHERE RequestId = {1} AND NurseId IS NULL",
                nurseId, requestId);

            if (rowsAffected == 0)
                return null;

            return await _dbContext.Requests
                .Include(r => r.Patient)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);
        }
        public async Task<Request?> UpdateRequestStatuAsync(int requestId, RequestStatus status)
        {
            var rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(
                "UPDATE Requests SET Status = {0} WHERE RequestId = {1}",
                status, requestId);

            if (rowsAffected == 0)
                return null;

            return await _dbContext.Requests
                .Include(r => r.Patient)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);
        }

        public async Task<Request?> UpdateRequestAsync(UpdateRequestDTO dto)
        {
            var request = await GetByIdWithReletadData(dto.RequestId);
            if (request == null)
                return null;

            request.RequestDate = dto.RequestDate;
            request.PickupAddress = dto.PickupAddress;
            request.DropOffAddress = dto.DropOffAddress;
            request.ScheduledDate = dto.ScheduledDate;
            request.EmergencyType = dto.EmergencyType;

            await _dbContext.SaveChangesAsync();
            return request;
        }
        public async Task<Request?> GetByIdWithReletadData(int? requestId)
        {
            if (requestId == null || requestId <= 0)
                throw new ArgumentException("Invalid request ID", nameof(requestId));
            return await _dbContext.Requests
                .Include(r => r.Patient).ThenInclude(p => p.User)
                .Include(r => r.Driver).ThenInclude(d => d.User)
                .Include(r => r.Nurse).ThenInclude(n => n.User)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);
        }

        public async Task<bool> UpdatePatientConfirmed(int requestId)
        {
            var request = await _dbContext.Requests.FindAsync(requestId);
            if (request == null)
                return false;

            if (request.PatientConfirmed)
                return true;

            request.PatientConfirmed = true;
            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> IsDriverDoubleBookedAsync(int driverId, DateTime scheduledDate)
        {
            return await _dbContext.Requests.AnyAsync(r => r.DriverId == driverId && r.ScheduledDate == scheduledDate && r.Status != RequestStatus.Cancelled);
        }
        public async Task<bool> IsNurseDoubleBookedAsync(int nurseId, DateTime scheduledDate)
        {
            return await _dbContext.Requests.AnyAsync(r => r.NurseId == nurseId && r.ScheduledDate == scheduledDate && r.Status != RequestStatus.Cancelled);
        }
        public async Task<bool> IsAmbulanceDoubleBookedAsync(int ambulanceId, DateTime scheduledDate)
        {
            return await _dbContext.Requests.AnyAsync(r => r.AssignedAmbulanceId == ambulanceId && r.ScheduledDate == scheduledDate && r.Status != RequestStatus.Cancelled);
        }
        #endregion

    }
}
