using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public interface IAnonymousUserFacade
    {
        IList<Flight> GetAllFlights();

        IList<AirlineCompany> GetAllAirlineCompanies();

        Dictionary<Flight, int> GetAllFlightsVacancy();

        Flight GetFlightById(long id);

        IList<Flight> GetFlightsByOriginCountry(long countryCode);

        IList<Flight> GetFlightsByDestinationCountry(long countryCode);

        IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate);

        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);

        Ticket GetTicketById(long id);

    }
}
