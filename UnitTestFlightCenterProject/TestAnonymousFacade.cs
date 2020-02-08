using System;
using System.Collections.Generic;
using FlightCenterProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestFlightCenterProject
{
    [TestClass]
    public class TestAnonymousFacade
    {
        [TestMethod]
        public void AnonymouseFacade_GetFlightById_FlightFound()
        {
            AnonymousUserFacade facade = FlyingCenterSystem.GetInstance().GetAnonymousFacade();

            Flight f = facade.GetFlightById(TestResource.FLIGHT_ID);

            Assert.AreNotEqual(null, f);

            Assert.AreEqual(TestResource.FLIGHT_ID, f.Id);

            Assert.AreEqual(TestResource.AIRLINE_COMPANY_ID, f.AirlineCompanyId);

            Assert.AreEqual(TestResource.VACANCY, f.RemainingTickets);

            Assert.AreEqual(TestResource.ORIGIN_COUNTRY_CODE, f.OriginCountryCode);

            Assert.AreEqual(TestResource.DESTINATION_COUNTRY_CODE, f.DestinationCountryCode);

            Assert.AreEqual(TestResource.LANDING_TIME, f.LandingTime);

            Assert.AreEqual(TestResource.DEPARTURE_TIME, f.DepartureTime);

        }

        [TestMethod]
        public void AnonymouseFacade_GetAllFlightsVacancy_FlightsVacancyFound()
        {
            AnonymousUserFacade facade = FlyingCenterSystem.GetInstance().GetAnonymousFacade();

            Dictionary<Flight, int> flightsVacancy= facade.GetAllFlightsVacancy();

            Assert.AreNotEqual(flightsVacancy, null);

            IList<Flight> flights= facade.GetAllFlights();

            int i = 0;

            foreach (Flight flight in flights)
            {
                flightsVacancy.TryGetValue(flight, out int vacancy);

                Assert.AreEqual(TestResource.Vacancies[i], vacancy);

                i = i + 1;
            }
            
        }

        [TestMethod]
        public void AnonymouseFacade_GetFlightsByOriginCountry_FlightsFound()
        {
            AnonymousUserFacade facade = FlyingCenterSystem.GetInstance().GetAnonymousFacade();

           IList<Flight> flights = facade.GetFlightsByOriginCountry(TestResource.ORIGIN_COUNTRY_CODE);

            for (int i = 0; i < flights.Count; i++)
            {
                Assert.AreEqual(TestResource.ORIGIN_COUNTRY_CODE, flights[i].OriginCountryCode);
            }

        }

        [TestMethod]
        public void AnonymouseFacade_GetFlightsByDestinationCountry_FlightsFound()
        {
            AnonymousUserFacade facade = FlyingCenterSystem.GetInstance().GetAnonymousFacade();

            IList<Flight> flights = facade.GetFlightsByDestinationCountry(TestResource.DESTINATION_COUNTRY_CODE);

            for (int i = 0; i < flights.Count; i++)
            {
                Assert.AreEqual(TestResource.DESTINATION_COUNTRY_CODE, flights[i].DestinationCountryCode);
            }

        }

        [TestMethod]
        public void AnonymouseFacade_GetFlightsByDepatrureDate_FlightsFound()
        {
            AnonymousUserFacade facade = FlyingCenterSystem.GetInstance().GetAnonymousFacade();

            IList<Flight> flights = facade.GetFlightsByDepatrureDate(TestResource.DEPARTURE_TIME);

            Assert.AreNotEqual(null, flights);

            for (int i = 0; i < flights.Count; i++)
            {
                Assert.AreEqual(TestResource.FlightsId[i], flights[i].Id);

                Assert.AreEqual(TestResource.DepartureTimes[i], flights[i].DepartureTime);

                Assert.AreEqual(TestResource.LandingTimes[i], flights[i].LandingTime);

                Assert.AreEqual(TestResource.AirlineCompaniesId[i], flights[i].AirlineCompanyId);

                Assert.AreEqual(TestResource.OriginCountryCodes[i], flights[i].OriginCountryCode);

                Assert.AreEqual(TestResource.DestinationCountryCodes[i], flights[i].DestinationCountryCode);

                Assert.AreEqual(TestResource.Vacancies[i], flights[i].RemainingTickets);
            }
        }

        [TestMethod]
        public void AnonymouseFacade_GetFlightsByLandingDate_FlightsFound()
        {
            AnonymousUserFacade facade = FlyingCenterSystem.GetInstance().GetAnonymousFacade();

            IList<Flight> flights = facade.GetFlightsByLandingDate(TestResource.LANDING_TIME);

            Assert.AreNotEqual(null, flights);

            for (int i = 0; i < flights.Count; i++)
            {
                Assert.AreEqual(TestResource.FlightsId[i], flights[i].Id);

                Assert.AreEqual(TestResource.DepartureTimes[i], flights[i].DepartureTime);

                Assert.AreEqual(TestResource.LandingTimes[i], flights[i].LandingTime);

                Assert.AreEqual(TestResource.AirlineCompaniesId[i], flights[i].AirlineCompanyId);

                Assert.AreEqual(TestResource.OriginCountryCodes[i], flights[i].OriginCountryCode);

                Assert.AreEqual(TestResource.DestinationCountryCodes[i], flights[i].DestinationCountryCode);

                Assert.AreEqual(TestResource.Vacancies[i], flights[i].RemainingTickets);
            }

        }

        [TestMethod]
        public void AnonymouseFacade_GetAllFlights_FlightsFound()
        {
            AnonymousUserFacade facade = FlyingCenterSystem.GetInstance().GetAnonymousFacade();

            IList<Flight> flights = facade.GetAllFlights();

            Assert.AreNotEqual(null, flights);

            for (int i = 0; i < flights.Count; i++)
            {
                Assert.AreEqual(TestResource.FlightsId[i], flights[i].Id);

                Assert.AreEqual(TestResource.DepartureTimes[i], flights[i].DepartureTime);

                Assert.AreEqual(TestResource.LandingTimes[i], flights[i].LandingTime);

                Assert.AreEqual(TestResource.AirlineCompaniesId[i], flights[i].AirlineCompanyId);

                Assert.AreEqual(TestResource.OriginCountryCodes[i], flights[i].OriginCountryCode);

                Assert.AreEqual(TestResource.DestinationCountryCodes[i], flights[i].DestinationCountryCode);

                Assert.AreEqual(TestResource.Vacancies[i], flights[i].RemainingTickets);
            }

        }

        [TestMethod]
        public void AnonymouseFacade_GetAllAirlineCompanies_CompaniesFound()
        {
            AnonymousUserFacade facade = FlyingCenterSystem.GetInstance().GetAnonymousFacade();

            IList<AirlineCompany> companies = facade.GetAllAirlineCompanies();

            Assert.AreNotEqual(null, companies);

            for (int i = 0; i < companies.Count; i++)
            {
                Assert.AreEqual(TestResource.airlinesId[i], companies[i].Id);

                Assert.AreEqual(TestResource.airlineNames[i], companies[i].AirlineName);

                Assert.AreEqual(TestResource.airlineCountryCodes[i], companies[i].CountryCode);
               
            }

        }

       



    }
}
