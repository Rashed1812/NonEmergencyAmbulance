using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Trip_Module;
using Shared.DTOS.TripDTOs;

namespace Service.Mapping
{
    public static class TripMapping
    {
        public static TripDTO ToTripDTO(this Trip trip)
        {
            return new TripDTO
            {
                TripId = trip.TripId,
                StartTime = trip.StartTime,
                EndTime = trip.EndTime,
                PickupAddress = trip.Request.PickupAddress,
                DropOffAddress = trip.Request.DropOffAddress,
                DriverName = trip.Driver?.User.UserName ?? "",
                NurseName = trip.Nurse?.User.UserName ?? "",
                DistanceKM = trip.DistanceKM,
                Price = trip.Price,
                TripStatus = trip.TripStatus
            };
        }
    }
}
