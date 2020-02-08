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
    }
}   
