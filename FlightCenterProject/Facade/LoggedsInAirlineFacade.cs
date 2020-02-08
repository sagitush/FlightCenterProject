using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        /// <summary>
        /// Removes a flight from the data base.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {          
                if (token.User.Id == flight.AirlineCompanyId)
                {
                    _flightDAO.Remove(flight);
                }
                else
                {
                    throw new DoesNotBelongToThisAirlineCompanyException("the flight that you want to cancel is not belong to your airline company");
                }                       
        }

        /// <summary>
        /// Changes the password of the airline company.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
           AirlineCompany airlineCompany= _airlineDAO.GetAirlineByPassword(oldPassword);

            if (airlineCompany != null)
            {
                airlineCompany.Password = newPassword;

                _airlineDAO.Update(airlineCompany);
            }
            else
            {
                throw new WrongPasswordException("The old password is incorrect");
            }
            
        }

        /// <summary>
        /// Add a flight to the data base.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {          
                if (token.User.Id == flight.AirlineCompanyId)
                {
                    _flightDAO.Add(flight);
                }
                else
                {
                    throw new DoesNotBelongToThisAirlineCompanyException("The flight that you want to create does not belong to your airline company");
                }                     
        }

        /// <summary>
        /// Gives a list of the all flights of the airline company.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
           if(token!=null)
            {
                return _airlineDAO.GetFlightsOfTheAirline(token.User.Id);
            }
            return null;
        }

        /// <summary>
        /// Gives a list of all tickets of the airline company. 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
           if(token!=null)
           {
                return _airlineDAO.GetTicketsOfTheAirline(token.User.Id);
           }

            return null;         
        }
        
        /// <summary>
        /// Changes the details of the airline company.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void MofidyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if (token.User.Id == airline.Id)
            {
                _airlineDAO.Update(airline);
            }
            else
                throw new DoesNotBelongToThisAirlineCompanyException($"You can not change airline company: {airline.Id} because its not belong to you");
        }

        /// <summary>
        /// Changes the detailes of the flight.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
           
                if (token.User.Id == flight.AirlineCompanyId)
                {
                    _flightDAO.Update(flight);
                }
                else
                {
                    throw new DoesNotBelongToThisAirlineCompanyException("you can not update flight that not belong to your company");
                }
           
          
        }

      
    }
}
