using System.Collections.Generic;

namespace ParkingApp.Data.Domain.Abstract
{
    public interface IParkingsRepository
    {
        Entities.Parking GetParkingByName(string moniker);
        IEnumerable<Entities.Parking> GetAllParkings();
    }
}