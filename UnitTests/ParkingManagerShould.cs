using System;
using System.Collections.Generic;
using ParkingApp.Data.Domain.Entities;
using ParkingApp.Services;
using Xunit;

namespace UnitTests
{
    public class ParkingManagerShould
    {
        private readonly ParkingManager _parkingManager;
        private readonly Place _plc;
        private readonly ParkingSession _session;
      

        public ParkingManagerShould()
        {
            _session = new ParkingSession {StartTime = DateTime.Now};
            _plc = new Place
            {
                Row = 1,
                Column = 2,
                IsParkingLot = true,
                Bookings = new List<ParkingSession>()
            };
            _parkingManager = new ParkingManager(_plc, _session);
        }

        [Fact]
        public void CheckIfParkingOnThePlaceIsAvailable()
        {
            _plc.Occupied = true;
            Assert.False(_parkingManager.IsEmmediateParkingAllowed());
            _plc.Occupied = false;
            Assert.True(_parkingManager.IsEmmediateParkingAllowed());
        }

      
        [Fact]
        public void CheckIfParkingAvailableForBookedPlace()
        {
            var booking = new ParkingSession {StartTime = DateTime.MaxValue, IsBooking = true, Place = _plc};         
            _plc.Booked = true;
            _plc.Bookings.Add(booking);
            _session.DeclaredDurationInHours = 4;
            Assert.True(_parkingManager.IsEmmediateParkingAllowed());
        }

        [Theory]
        [InlineData(3,4)]
        public void CheckIfParkingAllowedForBookedLotUntilBooking(int ok, int tooLong)
        {
            var booking1 = new ParkingSession { StartTime = DateTime.Now.AddHours(4), IsBooking = true, Place = _plc };
            var booking2 = new ParkingSession { StartTime = DateTime.Now.AddHours(6), IsBooking = true, Place = _plc };
            _plc.Booked = true;
            _plc.Bookings.Add(booking2);
            _plc.Bookings.Add(booking1);

            _session.DeclaredDurationInHours = ok;

            Assert.True(_parkingManager.IsParkingAllowedUntilBooking());

            _session.DeclaredDurationInHours = tooLong;

            Assert.False(_parkingManager.IsParkingAllowedUntilBooking());
        }

        [Fact]
        public void ParkTheVehicle()
        {
            var vehicle = new Vehicle { Number = "a001aa77" };

            _parkingManager.Park(vehicle, DateTime.Now);

            Assert.True(_plc.Occupied);
            Assert.True(_session.Vehicle.Number == vehicle.Number);
        }

        [Fact]
        public void ReleaseTheVehicle()
        {
            var vehicle = new Vehicle { Number = "a001aa77" };
            _parkingManager.Park(vehicle, DateTime.Now, 4);
            _parkingManager.Release(vehicle);

            Assert.False(_session.Place.Occupied);
            Assert.True(DateTime.Compare(_session.StartTime,_session.ExitTime) != 0);

        }

        [Fact]
        public void BookTheVehicle()
        {
            var vehicle = new Vehicle { Number = "a001aa77" };
            var startTime = DateTime.Parse("12/01/2017");
            const int duration = 4;
 
            _parkingManager.Book(vehicle, startTime, duration);

            Assert.NotNull(_plc.Bookings);
            Assert.Contains(_plc.Bookings, session => 
                             session.Vehicle == vehicle 
                          && session.StartTime == startTime
                          && session.DeclaredDurationInHours == duration);
            Assert.True(_session.IsBooking);
            Assert.True(_session.Place.Booked);
        }
    }
}
