using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleApp.BL.Services;
using SimpleApp.Core.Contracts;
using SimpleApp.Core.Entities;
using SimpleApp.Core.Interfaces;

namespace SimpleApp.Test.Unit
{
    [TestClass]
    public class BookingService
    {

        [TestMethod]
        public void IsFree_ValidDateAndDuration_Returns_True()
        {
            // Arrange
            var bookings = new List<Booking>()
            {
                new Booking
                {
                    Id = 1,
                    HotelId = 1,
                    Start = DateTime.UtcNow.AddDays(-2),
                    End = DateTime.Now.AddDays(2)
                }
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repo => repo.GetAll<Booking>())
                .Returns(() => bookings);
            var bookingService = new BL.Services.BookingService(mockRepository.Object);

            // Act
            var result = bookingService.IsFree(new BookingContract
            {
                HotelId = 1,
                Start = DateTime.UtcNow.AddDays(3),
                Duration = 1
            });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFree_ZeroDuration_Throws_ArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repo => repo.GetAll<Booking>())
                .Returns(() => new List<Booking>());
            var bookingService = new BL.Services.BookingService(mockRepository.Object);

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => bookingService.IsFree(new BookingContract
            {
                HotelId = 1,
                Start = DateTime.UtcNow.AddDays(3),
                Duration = 0
            }));
        }
    }
}
