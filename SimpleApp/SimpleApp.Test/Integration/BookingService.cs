using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleApp.BL.Services;
using SimpleApp.Core.Contracts;
using SimpleApp.Core.Entities;
using SimpleApp.Core.Interfaces;
using SimpleApp.DAL;

namespace SimpleApp.Test.Integration
{
    [TestClass]
    public class BookingService
    {
        private IRepository repository;

        private IBookingService bookingService;

        [TestInitialize]
        public void Setup()
        {
            this.repository = new InFileRepository();
            this.bookingService = new BL.Services.BookingService(this.repository);
        }

        [TestCleanup]
        public void TearDown()
        {
            this.repository.GetAll<Booking>().ToList().ForEach(b => this.repository.Delete<Booking>(b.Id));
            this.repository = null;
            this.bookingService = null;
        }

        [TestMethod]
        public void Create_ValidData_Should_CreateBooking()
        {
            // Arrange
            this.repository.Create(new Booking { HotelId = 1, Start = DateTime.UtcNow.AddDays(-2), End = DateTime.UtcNow.AddDays(-1) });

            // Act
            var booking = new BookingContract
            {
                Start = DateTime.UtcNow.AddDays(1),
                Duration = 2,
                HotelId = 1
            };
            var result = this.bookingService.Book(booking);

            // Assert
            var bookingEntity = this.repository.FirstOrDefault<Booking>(x => x.Id == result);
            Assert.IsNotNull(bookingEntity);
            Assert.AreEqual(booking.HotelId, bookingEntity.HotelId);
            Assert.AreEqual(booking.Start, bookingEntity.Start);
            Assert.AreEqual(booking.Start.AddDays(booking.Duration), bookingEntity.End);
        }
    }
}
