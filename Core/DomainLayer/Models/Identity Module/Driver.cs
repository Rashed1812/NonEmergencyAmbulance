using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Ambulance_Module;
using DomainLayer.Models.Request_Module;
using DomainLayer.Models.Trip_Module;

namespace DomainLayer.Models.Identity_Module
{
    public class Driver
    {
        public int Id { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAvailable { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Request> AssignedRequests { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public ICollection<Ambulance> Ambulances { get; set; }
    }
}
