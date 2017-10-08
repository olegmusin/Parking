using System;
using System.Collections.Generic;
using System.Linq;
using ParkingApp.Data.Domain.Entities;

namespace ParkingApp.Data.Domain
{
    public partial class Repository<TContext>
    {
        public Place GetPlaceForParking(int row, int column, string parkingId)
        {
            return Get<Place>(p => p.Parking.Id == parkingId && p.Row == row && p.Column == column).First();
        }

        public IEnumerable<Place> GetAllPlacesForParking(string name)
        {
            return GetAll<Place>(p => p.Where(plc => plc.Parking.Name == name
                                                     && plc.IsParkingLot)
                                       .OrderBy(plc => plc.Parking.Name));
        }

        public DateTime GetNextBookingDateTime(Place place)
        {
            return Get<ParkingSession>(s => s.Place == place 
            && s.IsBooking, bs => bs.OrderByDescending(b => b.StartTime)).First().StartTime;
        }
    }
}
