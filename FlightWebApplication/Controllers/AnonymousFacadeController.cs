using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlightCenterProject;


namespace FlightWebApplication.Controllers
{
     public class AnonymousFacadeController : ApiController
     {
       static AnonymousUserFacade anonymous;

        static AnonymousFacadeController()
        {
            anonymous = new AnonymousUserFacade();
        }

        [Route("api/anonymousFacade/allFlights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
           IList<Flight> flights= anonymous.GetAllFlights();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [Route("api/anonymousFacade/allAirlines")]
        [HttpGet]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            IList<AirlineCompany> airlines = anonymous.GetAllAirlineCompanies();
            if (airlines.Count == 0)
                return NotFound();
            return Ok(airlines);
        }

        [ResponseType(typeof(Dictionary<Flight, int>))]
        [HttpGet]
        [Route("api/anonymousFacade/allFlightsVacancy")]       
        public IHttpActionResult GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> vacancy = anonymous.GetAllFlightsVacancy();
            if (vacancy.Count == 0)
                return NotFound();
            return Ok(vacancy);
           
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightsById/{id}")]      
        public IHttpActionResult GetFlightById([FromUri] int id)
        {
            Flight flight = anonymous.GetFlightById(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
           
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightsByOriginCountry/{countryCode}")]     
        public IHttpActionResult GetFlightsByOriginCountry([FromUri] int countryCode)
        {
            IList<Flight> flights = anonymous.GetFlightsByOriginCountry(countryCode);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightsByDestinationCountry/{countryCode}")]        
        public IHttpActionResult GetFlightsByDestinationCountry([FromUri] int countryCode)
        {
            IList<Flight> flights = anonymous.GetFlightsByDestinationCountry(countryCode);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightsByDepatrureDate/{departureDate}")]    
        public IHttpActionResult GetFlightsByDepatrureDate([FromUri] DateTime departureDate)
        {
            IList<Flight> flights = anonymous.GetFlightsByDepatrureDate(departureDate);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightsByLandingDate/{landingDate}")]       
        public IHttpActionResult GetFlightsByLandingDate([FromUri] DateTime landingDate)
        {
            IList<Flight> flights = anonymous.GetFlightsByLandingDate(landingDate);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }
     }
}
