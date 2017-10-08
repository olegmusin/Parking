using System;
using ParkingApp.Data.Domain.Abstract;

namespace ParkingApp.Data.Domain.Entities
{
    public class ParkingSession : Entity<string>
    {  
        public DateTime StartTime { get; set; }
        public DateTime ExitTime { get; set; }
        public int DeclaredDurationInHours { get; set; }
        public bool IsBooking { get; set; }

        public Vehicle Vehicle { get; set; }
        public Place Place { get; set; }
        public Parking Parking { get; set; }

    }
}
