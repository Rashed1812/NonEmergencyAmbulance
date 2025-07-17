using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.RequestDTOS;
using Shared.DTOS.TripDTOs;

namespace ServiceAbstraction
{
    public interface IRequestService
    {
        public Task<RequestDTO> AddNewRequest(string userId, CreateRequestDTO createRequestDTO); 
        public Task<IEnumerable<RequestDTO>> GetAllRequestsWithRelatedData();
        Task<IEnumerable<RequestDTO>> GetAvailableRequestsForDriverAsync();
        Task<IEnumerable<RequestDTO>> GetAvailableRequestsForNurseAsync();
        Task<AssignUpdateRequestDTO?> AssignDriverAsync(AssignDriverDTO dto);
        Task<AssignUpdateRequestDTO?> AssignNurseAsync(AssignNurseDTO dto);
        Task<AssignUpdateRequestDTO?> UpdateStatusAsync(UpdateRequestStatusDTO dto);
        public Task<RequestDTO?> UpdateRequest(UpdateRequestDTO requestDTO);
        public Task<RequestDTO?> GetRequestById(RequestDTO requestDTO);
        Task<TripDTO?> ConfirmPatientAsync(int requestId);
        Task<bool> CancelRequestAsync(int requestId);
    }
}
