using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public abstract class FacadeBase
    {
        protected IAirlineDAO _airlineDAO = new AirlineCompanyDAOMSSQL();

        protected ICountryDAO _countryDAO = new CountryDAOMSSQL();

        protected ICustomerDAO _customerDAO = new CustomerDAOMSSQL();

        protected IFlightDAO _flightDAO = new FlightDAOMSSQL();

        protected ITicketDAO _ticketDAO = new TicketDAOMSSQL();
    }
}
