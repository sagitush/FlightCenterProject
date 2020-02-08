using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
     public class FlightView
     {
        public long FlightNumber { get; set; }
        public string AirlineCompanyName { get; set; }
        public string OriginCountryName { get; set; }
        public string DestinationCountryName { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        
        
     }
}
