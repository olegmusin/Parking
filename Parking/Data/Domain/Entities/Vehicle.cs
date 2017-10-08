using ParkingApp.Data.Domain.Abstract;

namespace ParkingApp.Data.Domain.Entities
{
    public class Vehicle  : Entity<string>
    {
       public string Number { get; set; }
       
    }
}
