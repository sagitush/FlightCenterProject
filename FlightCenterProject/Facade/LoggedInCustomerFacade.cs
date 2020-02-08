using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        /// <summary>
        /// Removes a ticket from the data base.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ticket"></param>
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {

            if (token.User.Id == ticket.CustomerId)
            {
                _ticketDAO.Remove(ticket);
                Flight flight = _flightDAO.GetFlightById(ticket.FlightId);
                flight.RemainingTickets = flight.RemainingTickets + 1;
                _flightDAO.Update(flight);
            }
            else
                throw new DoesNotBelongToThisCustomerException($"you can not cancel ticket: {ticket.Id} because its not belong to you");

        }

        /// <summary>
        /// Gives a list of flights by customer.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            if (token != null)
            {
                return _flightDAO.GetFlightsByCustomer(token.User);
            }
            return null;
        }

        /// <summary>
        /// Cheks if the customer already has a ticket for this flight if not,
        /// Adds a ticket on his Id to the data base and return the ticket.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            Ticket ticket= _customerDAO.CheckIfCustomerHasCardForThisFlight(flight,token.User.Id);
            if (ticket != null)
            {
                throw new TheCustomerHasAlreadyATicketForThisFlightException($"The customer Id: {token.User.Id} has already a ticket for flight: {flight.Id}");
            }
            else
            {
                if (flight.RemainingTickets > 1)
                {
                    flight.RemainingTickets = flight.RemainingTickets - 1;
                    _flightDAO.Update(flight);
                    Ticket newTicket = new Ticket(flight.Id, token.User.Id);
                    _ticketDAO.Add(newTicket);
                    return newTicket;
                }
                else
                {
                    throw new OutOfTicketsException($"Out of tickets to flight: {flight.Id}");
                }
            }
           
        }
    }
}
