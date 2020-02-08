using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public interface IAirlineDAO:IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUserName(string name);
        IList<AirlineCompany> GetAllAirlinesByCountry(long countryId);
        IList<Ticket> GetTicketsOfTheAirline(long airlineId);
        IList<Flight> GetFlightsOfTheAirline(long airlineId);
        AirlineCompany GetAirlineByPassword(string password);
    }
}
