using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {
        /// <summary>
        /// Gives a list of all the airline companies.
        /// </summary>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }

        /// <summary>
        /// Gives a list of all the flights.
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }

        /// <summary>
        ///  Gives a dictionary of all flights and their vacancy.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            return _flightDAO.GetAllFlightsVacancy();
        }

        /// <summary>
        /// Gives a flight by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flight GetFlightById(long id)
        {
            return _flightDAO.GetFlightById(id);
        }

        /// <summary>
        /// Gives a list of flights by diparture date.
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepatrureDate(departureDate);
        }

        /// <summary>
        /// Gives a list of flights by destination country.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(countryCode);
        }

        /// <summary>
        /// Gives a list of flights by landing date.
        /// </summary>
        /// <param name="landingDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }

        /// <summary>
        /// Gives a list of flights by origin country.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByOriginCountry(long countryCode)
        {
            return _flightDAO.GetFlightsByOriginCountry(countryCode);
        }

        /// <summary>
        /// Gives a ticket by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ticket GetTicketById(long id)
        {
            return _ticketDAO.Get(id);
        }

        public IList<Flight> GetFlightsDepartureAtNext12Hours()
        {
            return _flightDAO.GetFlightsThatDepartureAtTheNext12Hours();
        }

        public IList<Flight> GetFlightsLandAtNext12Hours()
        {
            return _flightDAO.GetFlightsThatLandAtTheNext12Hours();
        }

        public string GetNameCountryById(long id)
        {
            return _countryDAO.Get(id).CountryName;
        }
        
        public IList<Flight> GetFlightsByFilter(long originCountry,long destinationCountry,long airlineCompany,long flightNum)
        {
            return _flightDAO.GetFoodsByFilter(originCountry, destinationCountry, airlineCompany, flightNum);
        }

        public IList<FlightView> GetFlightsLandAtNext12HoursAndLast4H()
        {
            return _flightViewDAO.GetFlightsThatLandAtTheNext12HoursAndLast4H();
            
        }

        public IList<FlightView> GetFlightsDepartureForNext12Hours()
        {
            return _flightViewDAO.GetFlightsThatDepartureAtTheNext12Hours();

        }

        public IList<FlightView> GetLandingAndDepartureFlights()
        {
            return _flightViewDAO.GetLandingAndDepartureFlightView();

        }

        public IList<FlightView> GetFlightsViewByFilter(int flightNum, string originCoun, string destCoun, string airline, string searchType)
        {
            return _flightViewDAO.GetFlightsViewByFilter(flightNum, originCoun, destCoun, airline, searchType);
      
        }


    }
}   
