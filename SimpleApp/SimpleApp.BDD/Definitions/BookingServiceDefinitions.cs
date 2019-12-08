using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleApp.Core.Entities;
using SimpleApp.Core.Interfaces;
using SimpleApp.DAL;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SimpleApp.BDD.Definitions
{
    [Binding]
    public sealed class BookingServiceDefinitions
    {
        private const string REPOSITORY = "Repository";
        private const string BOOKING_ID = "BookingId";

        private readonly ScenarioContext context;

        public BookingServiceDefinitions(ScenarioContext injectedContext)
        {
            context = injectedContext;
            context.Add(REPOSITORY, new InFileRepository());
        }

        [Given("The following bookings exist")]
        public void GivenTheFollowingBookingsExist(List<Booking> bookings)
        {
            var repository = context.Get<IRepository>(REPOSITORY);
            bookings.ForEach(x => repository.Create(x));
        }

        [When("I add boooking for hotel (.*) from (.*) to (.*)")]
        public void WhenIAddBooking(int hotelId, DateTime from, DateTime to)
        {
            var repository = this.context.Get<IRepository>(REPOSITORY);
            this.context.Add(BOOKING_ID, repository.Create(new Booking { HotelId = hotelId, Start = from, End = to }));
        }

        [Then("New booking added")]
        public void ThenTheResultShouldBe(int result)
        {
            Assert.IsTrue(this.context.ContainsKey(BOOKING_ID), "New booking Id is not present");
            var id = this.context.Get<int>(BOOKING_ID);
            var repo = this.context.Get<IRepository>(REPOSITORY);
            Assert.IsNotNull(repo.FirstOrDefault<Booking>(x => x.Id == id), "Booking does not exist in repository");
        }
    }
}
