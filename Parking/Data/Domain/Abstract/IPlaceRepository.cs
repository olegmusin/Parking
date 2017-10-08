using System;
using System.Collections.Generic;
using ParkingApp.Data.Domain.Entities;

namespace ParkingApp.Data.Domain.Abstract
{
    public interface IPlaceRepository
    {
        Place GetPlaceForParking(int row, int column, string parkingId);
        IEnumerable<Place> GetAllPlacesForParking(string name);
        DateTime GetNextBookingDateTime(Place place);
    }
}
