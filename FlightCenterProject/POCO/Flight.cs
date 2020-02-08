using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class Flight: IPoco
    {
        public long Id { get; set; }
        public long AirlineCompanyId { get; set; }
        public long OriginCountryCode { get; set; }
        public long DestinationCountryCode { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int RemainingTickets { get; set; }

        public Flight()
        {
                
        }

        public Flight(long airlineCompanyId, long originCountryCode, long destinationCountryCode, DateTime departureTime, DateTime landingTime, int remainingTickets)
        {
            AirlineCompanyId = airlineCompanyId;
            OriginCountryCode = originCountryCode;
            DestinationCountryCode = destinationCountryCode;
            DepartureTime = departureTime;
            LandingTime = landingTime;
            RemainingTickets = remainingTickets;
        }

        public static bool operator ==(Flight flight1, Flight flight2)
        {
            if (ReferenceEquals(flight1, null) && ReferenceEquals(flight2, null))
                return true;
            if (ReferenceEquals(flight1, null) || ReferenceEquals(flight2, null))
                return false;

            return (flight1.Id == flight2.Id);
        }
        public static bool operator !=(Flight flight1, Flight flight2)
        {
            return !(flight1 == flight2);
        }

        public override bool Equals(object ob)
        {
            if (ReferenceEquals(ob, null))
                return false;
            Flight c = ob as Flight;
            if (ReferenceEquals(c, null))
                return false;

            return this.Id == c.Id;
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(this.Id);
        }

    }
}
