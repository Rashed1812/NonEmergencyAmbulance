using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Request_Module;
using Shared.DTOS.RequestDTOS;

namespace Service.Mapping
{
    public static class RequestMapping
    {
        public static RequestDTO ToRequestDTO(this Request request)
        {
            return new RequestDTO
            {
                RequestId = request.RequestId,
                RequestDate = request.RequestDate,
                PickupAddress = request.PickupAddress,
                DropOffAddress = request.DropOffAddress,
                ScheduledDate = request.ScheduledDate,
                EmergencyType = request.EmergencyType,
                Status = request.Status,
                Notes = request.Notes,
                AssignedAmbulanceId = request.AssignedAmbulanceId,
                PatientId = request.PatientId,
                PatientName = request.Patient.User.FullName,
                PatientPhone = request.Patient?.PhoneNumber,
                PatientAddress = request.Patient?.Address,
                DriverId = request.DriverId,
                DriverName = request.Driver?.User?.FullName,
                DriverPhone = request.Driver?.PhoneNumber,
                AmbulancePlateNumber = request.Trip?.Ambulance?.PlateNumber,
                NursePhone = request.Nurse?.PhoneNumber,
                NurseId = request.NurseId,
                NurseName = request.Nurse?.User?.FullName,
            };
        }
        public static AssignUpdateRequestDTO ToAssignUpdateRequestDTO(this Request request)
        {
            return new AssignUpdateRequestDTO
            {
                RequestId = request.RequestId,
                Status = request.Status,
                DriverId = request.DriverId,
                NurseId = request.NurseId,
                PatientName = request.Patient?.User?.FullName,
                PatientPhone = request.Patient?.PhoneNumber
            };
        }
    }
}
