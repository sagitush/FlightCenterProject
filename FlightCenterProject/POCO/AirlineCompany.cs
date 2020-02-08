using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class AirlineCompany:IPoco, IUser
    {
        public long Id { get; set; }
        public string AirlineName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long CountryCode { get; set; }

        public AirlineCompany()
        {
                
        }

        public AirlineCompany(string airlineName, string userName, string password, long countryCode)
        {
            AirlineName = airlineName;
            UserName = userName;
            Password = password;
            CountryCode = countryCode;
        }

        public static bool operator ==(AirlineCompany airline1, AirlineCompany airline2)
        {
            if (ReferenceEquals(airline1, null) && ReferenceEquals(airline2, null))
                return true;
            if (ReferenceEquals(airline1, null) || ReferenceEquals(airline2, null))
                return false;

            return (airline1.Id == airline2.Id);
        }
        public static bool operator !=(AirlineCompany airline1, AirlineCompany airline2)
        {
            return !(airline1 == airline2);
        }

        public override bool Equals(object ob)
        {
            if (ReferenceEquals(ob, null))
                return false;
            AirlineCompany c = ob as AirlineCompany;
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
