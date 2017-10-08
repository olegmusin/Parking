using System.Threading.Tasks;
using ParkingApp.Data.Domain.Entities;

namespace ParkingApp.Data.Domain.Abstract
{
    public interface IVechicleRepository
    {
        Vehicle GetVechiclebyNumber(string number);
        Task<Vehicle> GetCreateVechicle(string number);
    }
}
