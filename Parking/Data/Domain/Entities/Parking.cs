using System.Collections.Generic;
using ParkingApp.Data.Domain.Abstract;

namespace ParkingApp.Data.Domain.Entities
{
    public class Parking : Entity<string>
    {
        public Location Location { get; set; }
        public Owner Owner { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }
}
