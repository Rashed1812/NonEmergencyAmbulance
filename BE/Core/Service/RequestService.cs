using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.Request_Module;
using Microsoft.EntityFrameworkCore;
using Service.Mapping;
using ServiceAbstraction;
using Shared;
using Shared.DTOS.RequestDTOS;

namespace Service
{
    public class RequestService(IRequestRepository _requestRepo ,IPatientRepository _patientRepo) : IRequestService
    {
        //Add New Request

        public async Task<IEnumerable<RequestDTO>> GetAllRequestsWithRelatedData()
        {
            var requests = await _requestRepo.GetAllWithIncludeData();
            return requests.Select(r =>r.ToRequestDTO());
        }
        public async Task<IEnumerable<RequestDTO>> GetAvailableRequestsForDriverAsync()
        {
            var requests = _requestRepo.GetAvailableRequestsForDriverAsync();
            var list = await requests.ToListAsync();
            return list.Select(r => r.ToRequestDTO());
        }

        public async Task<IEnumerable<RequestDTO>> GetAvailableRequestsForNurseAsync()
        {
            var requests = _requestRepo.GetAvailableRequestsForNurseAsync();
            var list = await requests.ToListAsync();
            return list.Select(r => r.ToRequestDTO());
        }
        public async Task<RequestDTO> UpdateRequest(UpdateRequestDTO requestDTO)
        {
            var updatedRequest = await _requestRepo.UpdateRequestAsync(requestDTO);
            if (updatedRequest == null)
                throw new Exception("No Request With This Info");

            return updatedRequest.ToRequestDTO();
        }

        public async Task<AssignUpdateRequestDTO?> AssignDriverAsync(AssignDriverDTO assignDriverDTO)
        {
            var request = await _requestRepo.AddDriverToRequestAsync(assignDriverDTO.RequestId, assignDriverDTO.DriverId);
            if (request == null)
                return null;
            if (request.NurseId != null)
            {
                request.Status = RequestStatus.InProgress;
                await _requestRepo.SaveChangesAsync();
            }

            return request.ToAssignUpdateRequestDTO();
        }

        public async Task<AssignUpdateRequestDTO?> AssignNurseAsync(AssignNurseDTO assignNurseDTO)
        {
            var request = await _requestRepo.AddNurseToRequestAsync(assignNurseDTO.RequestId, assignNurseDTO.NurseId);
            if (request == null)
                return null;

            if (request.DriverId != null)
            {
                request.Status = RequestStatus.InProgress;
                await _requestRepo.SaveChangesAsync();
            }

            return request.ToAssignUpdateRequestDTO();
        }

        public async Task<AssignUpdateRequestDTO?> UpdateStatusAsync(UpdateRequestStatusDTO updateRequestStatusDTO)
        {
            var request = await _requestRepo.UpdateRequestStatuAsync(updateRequestStatusDTO.RequestId, updateRequestStatusDTO.Status);
            return request?.ToAssignUpdateRequestDTO();
        }

        public async Task<RequestDTO?> GetRequestById(RequestDTO requestDTO)
        {
            var request = await _requestRepo.GetByIdWithReletadData(requestDTO.RequestId);
            return request?.ToRequestDTO();
        }

        public async Task<RequestDTO> AddNewRequest(string userId, CreateRequestDTO createRequestDTO)
        {
            var patient = await _patientRepo.GetPatientByUserIdAsync(userId);
            if (patient == null)
                throw new Exception("المريض غير موجود");

            var newRequest = new Request
            {
                RequestDate = DateTime.UtcNow,
                PickupAddress = createRequestDTO.PickupAddress,
                DropOffAddress = createRequestDTO.DropOffAddress,
                ScheduledDate = createRequestDTO.ScheduledDate,
                EmergencyType = createRequestDTO.EmergencyType,
                Notes = createRequestDTO.Notes,
                AssignedAmbulanceId = createRequestDTO.AssignedAmbulanceId,
                Status = RequestStatus.Pending,
                PatientId = patient.Id
            };
            await _requestRepo.AddAsync(newRequest);
            await _requestRepo.SaveChangesAsync();

            var createdRequest = await _requestRepo.GetByIdWithReletadData(newRequest.RequestId);
            return createdRequest.ToRequestDTO();
        }
    }
}
