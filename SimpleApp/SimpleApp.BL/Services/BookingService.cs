using SimpleApp.Core.Interfaces;
using SimpleApp.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleApp.Core.Entities;

namespace SimpleApp.BL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository repository;

        public BookingService(IRepository repository)
        {
            this.repository = repository;
        }

        public int Book(BookingContract booking)
        {
            return this.IsFree(booking)
                ? this.repository.Create(new Booking { HotelId = booking.HotelId, Start = booking.Start, End = booking.Start.AddDays(booking.Duration) })
                : 0;
        }

        public bool Cancel(int id)
        {
            return this.repository.Delete<Booking>(id);
        }

        public bool IsFree(BookingContract booking)
        {
            if (booking.Duration < 1)
            {
                throw new ArgumentException("Duration is less than 1");
            }

            var bookingEnd = booking.Start.AddDays(booking.Duration);

            var bookings = this.repository.GetAll<Booking>(b => 
                b.HotelId == booking.HotelId &&
                (
                    (b.Start >= booking.Start && b.Start <= bookingEnd)
                    || (b.End >= booking.Start && b.End <= bookingEnd)
                    || (b.Start <= booking.Start && b.End >= bookingEnd)
                )
            );

            return bookings.Count() == 0;
        }
    }
}
