using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.TripDTOs
{
    public class RatingDTO
    {
            public int Id { get; set; }
            public int TripId { get; set; }
            public int? DriverId { get; set; }
            public int? NurseId { get; set; }
            public int PatientId { get; set; }
            public int Score { get; set; } 
            public string Notes { get; set; }
            public DateTime CreatedAt { get; set; }
        
    }
}
