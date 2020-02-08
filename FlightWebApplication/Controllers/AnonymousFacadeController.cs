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
     public class AnonymousFacadeController : ApiController
     {
        private ApiFacade apiFacade = new ApiFacade();
        private static AnonymousUserFacade anonymous;

        static AnonymousFacadeController()
        {
            anonymous = new AnonymousUserFacade();
        }

        /// <summary>
        /// Get all flights
        /// </summary>
        /// <returns></returns>
        [Route("api/anonymousFacade/allFlights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
           IList<Flight> flights= anonymous.GetAllFlights();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        /// <summary>
        /// Get all airlines
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IList<AirlineCompany>))]
        [Route("api/anonymousFacade/allAirlines")]
        [HttpGet]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            IList<AirlineCompany> airlines = anonymous.GetAllAirlineCompanies();
            if (airlines.Count == 0)
                return NotFound();
            return Ok(airlines);
        }

        /// <summary>
        /// Get all flights vacancy
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get specific flight by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsById/{id}")]      
        public IHttpActionResult GetFlightById([FromUri] int id)
        {
            Flight flight = anonymous.GetFlightById(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);          
        }

        /// <summary>
        /// Get Flights by origin country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsByOriginCountry/{countryCode}")]     
        public IHttpActionResult GetFlightsByOriginCountry([FromUri] int countryCode)
        {
            IList<Flight> flights = anonymous.GetFlightsByOriginCountry(countryCode);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        /// <summary>
        /// Get flights by destination country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsByDestinationCountry/{countryCode}")]        
        public IHttpActionResult GetFlightsByDestinationCountry([FromUri] int countryCode)
        {
            IList<Flight> flights = anonymous.GetFlightsByDestinationCountry(countryCode);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        /// <summary>
        /// Get flights departure date
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsByDepatrureDate/{departureDate}")]    
        public IHttpActionResult GetFlightsByDepatrureDate([FromUri] DateTime departureDate)
        {
            IList<Flight> flights = anonymous.GetFlightsByDepatrureDate(departureDate);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        /// <summary>
        /// Get flights landing date
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsByLandingDate/{landingDate}")]       
        public IHttpActionResult GetFlightsByLandingDate([FromUri] DateTime landingDate)
        {
            IList<Flight> flights = anonymous.GetFlightsByLandingDate(landingDate);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightsDeparture")]
        public IHttpActionResult GetFlightsThatDepartureAtTheNext12Hours()
        {
            IList<Flight> flights = anonymous.GetFlightsDepartureAtNext12Hours();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightsLand")]
        public IHttpActionResult GetFlightsThatLandAtTheNext12Hours()
        {
            IList<Flight> flights = anonymous.GetFlightsLandAtNext12Hours();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightsDepartureAndLanding")]
        public IHttpActionResult GetFlightsThatLandAndDepartureAtTheNext12Hours()
        {
            IList<Flight> flights1 = anonymous.GetFlightsDepartureAtNext12Hours();
            IList<Flight> flights2 = anonymous.GetFlightsLandAtNext12Hours();
            var flights = flights1.Concat(flights2);
            if (flights.Count() == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        // ...api/anonymousFacade/AirlineNamebyid ? id=19
        [Route("api/anonymousFacade/AirlineNamebyid")]
        public IHttpActionResult GetAirlineNameByid([FromUri] long id)
        {
            string name = apiFacade.GetMyCompany(id).AirlineName;
            if (name == "")
                return NotFound();
            return Ok(name);
        }

        [HttpGet]
        // ...api/anonymousFacade/CountryNameById ? id=19
        [Route("api/anonymousFacade/CountryNameById")]
        public IHttpActionResult GetCountryNameById([FromUri] long id)
        {
            string name = anonymous.GetNameCountryById(id);
            if (name == "")
                return NotFound();
            return Ok(name);
        }

        [HttpGet]
        [Route("api/anonymousFacade/OriginCountriesNames")]
        public IHttpActionResult GetOriginCountryNames()
        {
            Dictionary<long, string> numberAndCountryDict = new Dictionary<long, string>();
            IList<Flight> flights1 = anonymous.GetFlightsDepartureAtNext12Hours();
            IList<Flight> flights2 = anonymous.GetFlightsLandAtNext12Hours();
            var flights=flights1.Concat(flights2);
            if (flights.Count() == 0)
                return NotFound();
            foreach(Flight f in flights)
            {
                long Id = f.OriginCountryCode;
                string name = anonymous.GetNameCountryById(f.OriginCountryCode);
                if (numberAndCountryDict.ContainsKey(Id))
                {
                    continue;
                }
                else
                {
                    numberAndCountryDict.Add(Id, name);
                }
            }
            return Ok(numberAndCountryDict);
        }

        [HttpGet]
        [Route("api/anonymousFacade/AirlinesNames")]
        public IHttpActionResult GetAirlinesNames()
        {
            Dictionary<long, string> numberAndAirlineDict = new Dictionary<long, string>();            
            IList<Flight> flights1 = anonymous.GetFlightsDepartureAtNext12Hours();
            IList<Flight> flights2 = anonymous.GetFlightsLandAtNext12Hours();
            var flights = flights1.Concat(flights2);
            if (flights.Count() == 0)
                return NotFound();
            foreach (Flight f in flights)
            {
                long Id = f.AirlineCompanyId;
                string name = apiFacade.GetMyCompany(f.AirlineCompanyId).AirlineName;
                if (numberAndAirlineDict.ContainsKey(Id))
                {
                    continue;
                }
                else
                {
                    numberAndAirlineDict.Add(Id, name);
                }
            }
            return Ok(numberAndAirlineDict);
       
        }

        [HttpGet]
        [Route("api/anonymousFacade/DestinationCountriesNames")]
        public IHttpActionResult GetDestinationCountryNames()
        {
            Dictionary<long, string> numberAndCountryDict = new Dictionary<long, string>();          
            IList<Flight> flights1 = anonymous.GetFlightsDepartureAtNext12Hours();
            IList<Flight> flights2 = anonymous.GetFlightsLandAtNext12Hours();
            var flights = flights1.Concat(flights2);
            if (flights.Count() == 0)
                return NotFound();
            foreach (Flight f in flights)
            {
                long Id = f.DestinationCountryCode;
                string name = anonymous.GetNameCountryById(f.DestinationCountryCode);
                if (numberAndCountryDict.ContainsKey(Id))
                {
                    continue;
                }
                else
                {
                    numberAndCountryDict.Add(Id, name);                    
                }
            }
            return Ok(numberAndCountryDict);
        }
        
        [HttpGet]
        // ...api/anonymousFacade/AirlineNamebyid ? orgCoun=19 & destCoun=16 & airline=7 & flightNum=3
        [Route("api/anonymousFacade/FlightSearch")]
        public IHttpActionResult GetFlightNameByFilter(long orgCoun=0, long destCoun=0, long airline=0, long flightNum=0)
        {
            IList<Flight> flightsByFilter = anonymous.GetFlightsByFilter(orgCoun, destCoun, airline, flightNum);
            if(flightsByFilter.Count==0)
            {
                Console.WriteLine("No flights were found to match the search terms");
                return NotFound();
            }
            return Ok(flightsByFilter);
           
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightsLanding")]
        public IHttpActionResult GetFlightsThatLandAtTheNext12HoursAndLast4H()
        {
            IList<FlightView> flights = anonymous.GetFlightsLandAtNext12HoursAndLast4H();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightDeparture")]
        public IHttpActionResult GetFlightsThatDepartureForTheNext12Hours()
        {
            IList<FlightView> flights = anonymous.GetFlightsDepartureForNext12Hours();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        [Route("api/anonymousFacade/flightDepartureAndLandingView")]
        public IHttpActionResult GetLandingAndDepartureFlights()
        {
            IList<FlightView> flights = anonymous.GetLandingAndDepartureFlights();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        [HttpGet]
        // ...api/anonymousFacade/AirlineNamebyid ? orgCoun='israel' & destCoun='italy' & airline='elal' & flightNum=3 &searchType='landing'
        [Route("api/anonymousFacade/FlightsSearch")]
        public IHttpActionResult GetFlightsByFilter(string orgCoun = "", string destCoun= "", string airline = "", int flightNum = 0, string searchType="")
        {
            if (orgCoun == null)
            {
                orgCoun = "";
            }
            if (destCoun == null)
            {
                destCoun = "";
            }
            if (airline == null)
            {
                airline = "";
            }
            IList<FlightView> flightsByFilter = anonymous.GetFlightsViewByFilter(flightNum,orgCoun,destCoun,airline,searchType);
            if (flightsByFilter.Count == 0)
            {
                Console.WriteLine("No flights were found to match the search terms");
                return NotFound();
            }
            return Ok(flightsByFilter);

        }
    }
}
