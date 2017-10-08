using System.Collections.Generic;
using ParkingApp.Data.Domain.Abstract;
using ParkingApp.Data.Domain.Identity;

namespace ParkingApp.Data.Domain.Entities
{
    public class Owner : Entity<string>
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }

        public AppUser User { get; set; }

        public ICollection<Parking> Parkings { get; set; }
    }
}