using System.Collections.Generic;
using ParkingApp.Data.Domain.Entities;

namespace ParkingApp.Data.Domain
{
    public partial class Repository<TContext>
    {

        public Parking GetParkingByName(string name)
        {
            return GetOne<Parking>(p => p.Name == name, includeProperties: "Places");
        }

        public IEnumerable<Parking> GetAllParkings()
        {
            return GetAll<Parking>(includeProperties: "Places,Location");
        }

     
    }
}
