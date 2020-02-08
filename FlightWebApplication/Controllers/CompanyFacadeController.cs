using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlightCenterProject;
using FlightCenterProject.Facade;

namespace FlightWebApplication.Controllers
{
   [BasicAuthenticationCompany]
    public class CompanyFacadeController : ApiController
    {
        private static ApiFacade apiFacade;
      
        static CompanyFacadeController()
        {          
            apiFacade = new ApiFacade();
        }

        public LoginToken<AirlineCompany> GetAirlineLoginToken()
        {
            Request.Properties.TryGetValue("airlineToken", out object loginUser);
            LoginToken<AirlineCompany> airlineLoginToken = (LoginToken<AirlineCompany>)loginUser;
            return airlineLoginToken;
        }

        public LoggedInAirlineFacade GetAirlineFacade()
        {
           
            Request.Properties.TryGetValue("airlineFacade", out object facade);
            return (LoggedInAirlineFacade)facade;
        }

        /// <summary>
        /// Get all tickets of specific airline
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(List<Ticket>))]
        [HttpGet]
        [Route("api/companyFacade/getAllTickets")]       
        public IHttpActionResult GetAllTickets()
        {
            try
            {
                IList<Ticket> tickets = GetAirlineFacade().GetAllTickets(GetAirlineLoginToken());
                if (tickets.Count == 0)
                    return NotFound();
                return Ok(tickets);
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }          
        }

        /// <summary>
        /// Get all flights of specific airline
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IList<Flight>))]
        [HttpGet]
        [Route("api/companyFacade/getAllFlights")]       
        public IHttpActionResult GetAllFlights()
        {
            try
            {
                IList<Flight> flights = GetAirlineFacade().GetAllFlights(GetAirlineLoginToken());
                if (flights.Count == 0)
                    return NotFound();
                return Ok(flights);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Create new flight of specific airline
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/CompanyFacade/CreateFlight")]
        public IHttpActionResult CreateFlight([FromBody] Flight flight)
        {
            try
            {
                if (flight == null)
                    return BadRequest(ModelState);
                GetAirlineFacade().CreateFlight(GetAirlineLoginToken(), flight);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
           
        /// <summary>
        /// Update flight details 
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/CompanyFacade/updateFlight/{id}")]       
        public IHttpActionResult UpdateFlight([FromBody] Flight flight,[FromUri] long id)
        {
            try
            {
                if (apiFacade.GetMyFlight(id) == null)
                    return BadRequest(ModelState);
                else
                {
                    flight.Id = id;
                    GetAirlineFacade().UpdateFlight(GetAirlineLoginToken(), flight);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Change the password of the airline
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/CompanyFacade/ChangeMyPassword/{oldPassword}/{newPassword}")]
        public IHttpActionResult ChangeMyPassword([FromUri] string oldPassword,[FromUri] string newPassword)
        {
            try
            {
                GetAirlineFacade().ChangeMyPassword(GetAirlineLoginToken(), oldPassword, newPassword);
                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest(exp.ToString());
            }
        }

        /// <summary>
        /// Update the airline details
        /// </summary>
        /// <param name="airline"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/CompanyFacade/mofidyAirlineDetails")]        
        public IHttpActionResult MofidyAirlineDetails([FromBody] AirlineCompany airline,[FromUri] long id)
        {
            try
            {
                if (apiFacade.GetMyCompany(id) == null)
                    return BadRequest(ModelState);
                else
                {
                    airline.Id = id;
                    GetAirlineFacade().MofidyAirlineDetails(GetAirlineLoginToken(), airline);
                    return Ok();
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.ToString());
            }
        }

        /// <summary>
        /// Cancel flight that belong to this airline
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/CompanyFacade/CancelFlight/{id}")]
        public IHttpActionResult CancelFlight(long flightId)
        {
            try
            {
                Flight flight = apiFacade.GetMyFlight(flightId);
                if (flight == null)
                    return BadRequest(ModelState);
                GetAirlineFacade().CancelFlight(GetAirlineLoginToken(), flight);
                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest(exp.ToString());
            }
        }
    }
}
