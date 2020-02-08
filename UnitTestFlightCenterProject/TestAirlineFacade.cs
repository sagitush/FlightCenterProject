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
    public class TestAirlineFacade
    {
        [TestMethod]
        public void AirlineFacade_GetAllAirlineCompanies_CompaniesFound()
        {
            LoggedInAirlineFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.AIRLINE_USERNAME, TestResource.AIRLINE_PASSWORD, out LoginTokenBase login) as LoggedInAirlineFacade;

            IList<AirlineCompany> companies = new List<AirlineCompany>();

            companies = facade.GetAllAirlineCompanies();

            Assert.AreNotEqual(companies, null);

            for (int i = 0; i < companies.Count; i++)
            {

                Assert.AreEqual(TestResource.airlinesId[i], companies[i].Id);

                Assert.AreEqual(TestResource.airlineNames[i], companies[i].AirlineName);

                Assert.AreEqual(TestResource.airlineCountryCodes[i], companies[i].CountryCode);
            }

        }

        [TestMethod]
        public void AirlineFacade_CreateFlight()
        {
            LoggedInAirlineFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.AIRLINE_USERNAME, TestResource.AIRLINE_PASSWORD, out LoginTokenBase login) as LoggedInAirlineFacade;

            Flight flight = new Flight(1, 3, 2, new DateTime(2019, 08, 11, 09, 00, 00), new DateTime(2019, 08, 11, 12, 00, 00), 40);

            facade.CreateFlight(login as LoginToken<AirlineCompany>, flight);

        }

        [TestMethod]
        public void AirlineFacade_MofidyAirlineDetails()
        {
            LoggedInAirlineFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.AIRLINE_USERNAME, TestResource.AIRLINE_PASSWORD, out LoginTokenBase login) as LoggedInAirlineFacade;

            IList<AirlineCompany> companies = facade.GetAllAirlineCompanies();

            AirlineCompany company1 = companies[0];

            company1.CountryCode = 3;

            facade.MofidyAirlineDetails(login as LoginToken<AirlineCompany>, company1);

            Assert.AreEqual(3, company1.CountryCode);

            Assert.AreEqual(TestResource.AIRLINE_NAME, company1.AirlineName);
        }
              
        [TestMethod]
        public void AirlineFacade_CancelFlight()
        {
            LoggedInAirlineFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.AIRLINE_USERNAME, TestResource.AIRLINE_PASSWORD, out LoginTokenBase login) as LoggedInAirlineFacade;

           Flight flight= facade.GetFlightById(6);

            facade.CancelFlight(login as LoginToken<AirlineCompany>, flight);           
        }

        [TestMethod]
        public void AirlineFacade_ChangeMyPassword()
        {
            LoggedInAirlineFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.AIRLINE_USERNAME, TestResource.AIRLINE_PASSWORD, out LoginTokenBase login) as LoggedInAirlineFacade;

            facade.ChangeMyPassword(login as LoginToken<AirlineCompany>, "777", "666");
            
           
        }

        [TestMethod]
        public void AirlineFacade_GetAllTickets_TicketsFound()
        {
            LoggedInAirlineFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.AIRLINE_USERNAME, TestResource.AIRLINE_PASSWORD, out LoginTokenBase login) as LoggedInAirlineFacade;

            IList<Ticket> tickets= facade.GetAllTickets(login as LoginToken<AirlineCompany>);

            Assert.AreNotEqual(null, tickets);

            for (int i = 0; i < tickets.Count; i++)
            {
                Assert.AreEqual(TestResource.ticketsId[i], tickets[i].Id);

                Assert.AreEqual(TestResource.ticketFlightsId[i], tickets[i].FlightId);

                Assert.AreEqual(TestResource.ticketsCustomersId[i], tickets[i].CustomerId);
            }
        }

        [TestMethod]
        public void AirlineFacade_UpdateFlight()
        {
            LoggedInAirlineFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.AIRLINE_USERNAME, TestResource.AIRLINE_PASSWORD, out LoginTokenBase login) as LoggedInAirlineFacade;

            Flight flight = facade.GetFlightById(38);

            flight.DestinationCountryCode = 1;

            facade.UpdateFlight(login as LoginToken<AirlineCompany>, flight);

        }

        
    }
}
