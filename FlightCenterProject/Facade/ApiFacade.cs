using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject.Facade
{
    public class ApiFacade:FacadeBase
    {
        public Flight GetMyFlight(long id)
        {
           return _flightDAO.GetFlightById(id);
        }

        public Ticket GetMyTicket(long id)
        {
            return _ticketDAO.Get(id);
        } 

        public AirlineCompany GetMyCompany(long id)
        {
            return _airlineDAO.Get(id);
        }

        public Customer GetMyCustomer(long id)
        {
            return _customerDAO.Get(id);
        }
    }
}
