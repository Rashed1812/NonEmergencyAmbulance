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
using Shared.DTOS.TripDTOs;

namespace Service
{
    public class RequestService(IRequestRepository _requestRepo, IPatientRepository _patientRepo, ITripService _tripService, IAmbulanceRepository ambulanceRepository) : IRequestService
    {
        private readonly IAmbulanceRepository _ambulanceRepository;
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

            // Auto-assign ambulance linked to this driver
            var ambulance = await _ambulanceRepository.GetAllWithRelatedData();
            var driverAmbulance = ambulance.FirstOrDefault(a => a.DriverId == assignDriverDTO.DriverId);
            if (driverAmbulance != null)
            {
                request.AssignedAmbulanceId = driverAmbulance.AmbulanceId;
                await _requestRepo.SaveChangesAsync();
            }

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

            // Double booking validation
            if (createRequestDTO.AssignedAmbulanceId > 0)
            {
                if (await _requestRepo.IsAmbulanceDoubleBookedAsync(createRequestDTO.AssignedAmbulanceId, createRequestDTO.ScheduledDate))
                    throw new Exception("سيارة الإسعاف محجوزة في هذا الموعد!");
            }

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

        public async Task<TripDTO?> ConfirmPatientAsync(int requestId)
        {
            var request = await _requestRepo.GetByIdAsync(requestId);
            if (request == null)
                return null;

            if (!request.PatientConfirmed)
            {
                request.PatientConfirmed = true;
                await _requestRepo.SaveChangesAsync();
            }
            var tripDto = await _tripService.CreateTripFromRequestAsync(requestId);
            return tripDto;
        }

        public async Task<bool> CancelRequestAsync(int requestId)
        {
            var request = await _requestRepo.GetByIdWithReletadData(requestId);
            if (request == null) return false;
            request.Status = Shared.RequestStatus.Cancelled;
            await _requestRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RequestDTO>> GetRequestsByUserIdAsync(string userId)
        {
            var patient = await _patientRepo.GetPatientByUserIdAsync(userId);
            if (patient == null)
                throw new Exception("المريض غير موجود");
            var requests = await _requestRepo.GetRequestsByUserIdAsync(patient.UserId);
            return requests.Select(r => r.ToRequestDTO());
        }
    }
}
