using System.Linq;
using System.Threading.Tasks;
using ParkingApp.Data.Domain.Entities;

namespace ParkingApp.Data.Domain
{
    public class ParkingDbInitializer
    {
        private ParkingDbContext _ctx;
        public ParkingDbInitializer(ParkingDbContext ctx)
        {
            _ctx = ctx;
            
        }
        public async Task Seed()
        {
            if (!_ctx.Parkings.Any())
            {

                await _ctx.AddRangeAsync(_sampleData);
                await _ctx.SaveChangesAsync();
            }
          
        }

     
        private object[] _sampleData =
        {
          
                new Parking
                {
                    Name = "Parking A",
                    Location = new Location
                    {
                      Address1  = "Demyana Bednogo str. 1",
                      PostalCode = "123423",
                      CityTown = "Moscow",
                      Country = "Russia",
                      StateProvince = "Moscow"
                    },
                    Owner = new Owner
                    {
                        Name = "Customer 1",
                        CompanyName = "Customer Company",
                        PhoneNumber = "+7(495)900-00-00",
                        WebsiteUrl = "customer@customercompany.com",
                       
                    }
                },

                new Parking
                {
                    Name = "Parking B",
                    Location = new Location
                    {
                      Address1  = "Karamyshevskaya emb. 12",
                      PostalCode = "123423",
                      CityTown = "Moscow",
                      Country = "Russia",
                      StateProvince = "Moscow"
                    },
                    Owner = new Owner
                    {
                        Name = "Customer 2",
                        CompanyName = "Customer2 Company",
                        PhoneNumber = "+7(495)900-11-11",
                        WebsiteUrl = "customer2@customercompany.com",
                    
                    }
                }
            
        };

        
    }
}