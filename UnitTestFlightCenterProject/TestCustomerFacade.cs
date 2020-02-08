using FlightCenterProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFlightCenterProject
{
    [TestClass]
    public class TestCustomerFacade
    {
        [TestMethod]
        public void CustomerFacade_GetAllMyFlights_FlightsFound()
        {
            LoggedInCustomerFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.CUSTOMER_USERNAME, TestResource.CUSTOMER_PASSWORD, out LoginTokenBase login) as LoggedInCustomerFacade;

            IList<Flight> flights = new List<Flight>();

            flights = facade.GetAllMyFlights(login as LoginToken<Customer>);

            Assert.AreNotEqual(flights, null);

            for (int i = 0; i < flights.Count; i++)
            {
                Assert.AreEqual(TestResource.FlightsId[i], flights[i].Id);
                Assert.AreEqual(TestResource.AirlineCompaniesId[i], flights[i].AirlineCompanyId);
                Assert.AreEqual(TestResource.OriginCountryCodes[i], flights[i].OriginCountryCode);
                Assert.AreEqual(TestResource.DestinationCountryCodes[i], flights[i].DestinationCountryCode);
                Assert.AreEqual(TestResource.Vacancies[i], flights[i].RemainingTickets);
                Assert.AreEqual(TestResource.DepartureTimes[i], flights[i].DepartureTime);
                Assert.AreEqual(TestResource.LandingTimes[i], flights[i].LandingTime);
            }

        }

        [TestMethod]
        public void CustomerFacade_CancelTicket()
        {
            LoggedInCustomerFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.CUSTOMER_USERNAME, TestResource.CUSTOMER_PASSWORD, out LoginTokenBase login) as LoggedInCustomerFacade;

            Ticket ticket= facade.GetTicketById(19);

            facade.CancelTicket(login as LoginToken<Customer>, ticket);

        }

        [TestMethod]
        public void CustomerFacade_PurchaseTicket()
        {
            LoggedInCustomerFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.CUSTOMER_USERNAME, TestResource.CUSTOMER_PASSWORD, out LoginTokenBase login) as LoggedInCustomerFacade;

            Flight flight= facade.GetFlightById(7);

           Ticket ticket= facade.PurchaseTicket(login as LoginToken<Customer>, flight);

            Assert.AreEqual(7, ticket.FlightId);

            Assert.AreEqual(2, ticket.CustomerId);

        }


    }
}
