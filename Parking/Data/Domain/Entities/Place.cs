using System.Collections.Generic;
using ParkingApp.Data.Domain.Abstract;

namespace ParkingApp.Data.Domain.Entities
{
    public class Place : Entity<string>
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public virtual Parking Parking { get; set; }
        public virtual ICollection<ParkingSession> Bookings { get; set; }

        public bool Booked { get; set; }
        public bool Occupied { get; set; }
        public bool IsParkingLot { get; set; }
    }

}