using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Request_Module;
using DomainLayer.Models.Trip_Module;

namespace DomainLayer.Models.Identity_Module
{
    public class Nurse
    {
        public int Id { get; set; }
        public string Certification { get; set; }
        public bool IsAvailable { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Request> AssignedRequests { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}
