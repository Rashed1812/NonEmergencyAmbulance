using System;
using DomainLayer.Models.Identity_Module;

namespace DomainLayer.Models.Trip_Module
{
    public class Rating
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int? DriverId { get; set; }
        public Driver Driver { get; set; }
        public int? NurseId { get; set; }
        public Nurse Nurse { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int Score { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
} 