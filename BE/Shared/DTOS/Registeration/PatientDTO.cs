using System;
using Shared;

namespace Shared.DTOS.Registeration
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string MedicalHistory { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserId { get; set; }
    }
} 