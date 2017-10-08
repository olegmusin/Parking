using System;
using System.Threading.Tasks;
using ParkingApp.Data.Domain.Entities;

namespace ParkingApp.Data.Domain
{
    public partial class Repository<TContext>
    {
        public Vehicle GetVechiclebyNumber(string number)
        {
            return GetOne<Vehicle>(v => v.Number == number);
        }

        public async Task<Vehicle> GetCreateVechicle(string number)
        {
            var car = GetVechiclebyNumber(number);
            if (car == null)
            {
                car = new Vehicle { Number = number };

                Create(car);

                if (await SaveAsync())
                    return car;
                throw new Exception("Can't create vechicle!");
            }
            return car;
        }

    }
}
