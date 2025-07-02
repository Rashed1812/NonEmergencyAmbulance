using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Request_Module;
using Shared;

namespace DomainLayer.Models.Identity_Module
{
    public class Patient
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string MedicalHistory { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Request> Requests { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
