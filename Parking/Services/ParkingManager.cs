using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingApp.Data.Domain.Abstract;
using ParkingApp.Data.Domain.Entities;

namespace ParkingApp.Services
{
    public class ParkingManager
    {
        private Place _lot;
        private ParkingSession _session;


        public ParkingManager(Place lot, ParkingSession session)
        {
            _lot = lot;
            _session = session;
        }


        public bool IsEmmediateParkingAllowed()
        {
            return _lot.IsParkingLot
                   && !_lot.Occupied
                   && (!_lot.Booked || IsParkingAllowedUntilBooking());
        }

        public bool IsParkingAllowedUntilBooking()
        {
            if (!_lot.Bookings.Any()) return true;
            var nextBookingTime = _lot.Bookings.OrderBy(b => b.StartTime).First().StartTime;
            return DateTime.Now.AddHours(_session.DeclaredDurationInHours) < nextBookingTime;
        }


        public void Park(Vehicle vehicle, DateTime startTime, int durationInHours = 0)
        {
            InitializeSessionProps(vehicle, startTime, durationInHours, isBooking: false);

            if (!IsEmmediateParkingAllowed()) return;

            _lot.Occupied = true;
        }

        private void InitializeSessionProps(Vehicle vehicle, DateTime startTime, int durationInHours, bool isBooking)
        {
            _session.Place = _lot;
            _session.Vehicle = vehicle;
            _session.StartTime = startTime;
            _session.DeclaredDurationInHours = durationInHours;
            _session.IsBooking = isBooking;
        }

        public void Release(Vehicle vehicle)
        {
            _lot.Occupied = false;
            _session.ExitTime = DateTime.Now;
            var sessionHours = DateTime.Compare(_session.ExitTime, _session.StartTime);
        }

        public void Book(Vehicle vehicle, DateTime startTime, int duration)
        {

            InitializeSessionProps(vehicle, startTime, duration, isBooking: true);
            _session.Name = $"Lot #{_lot.Row}-{_lot.Column} " +
                            $"booked for {vehicle.Number} " +
                            $"starting at {startTime} for {duration} hrs";
            _session.ExitTime = startTime.AddHours(duration);
            _lot.Booked = true;
            _lot.Bookings.Add(_session);
        }
    }
}

