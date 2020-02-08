using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class Country: IPoco
    {
        public long Id { get; set; }
        public string CountryName { get; set; }

        public Country()
        {
                
        }

        public Country(string countryName)
        {
            CountryName = countryName;
        }

        public static bool operator ==(Country country1, Country country2)
        {
            if (ReferenceEquals(country1, null) && ReferenceEquals(country2, null))
                return true;
            if (ReferenceEquals(country1, null) || ReferenceEquals(country2, null))
                return false;

            return (country1.Id == country2.Id);
        }
        public static bool operator !=(Country country1, Country country2)
        {
            return !(country1 == country2);
        }

        public override bool Equals(object ob)
        {
            if (ReferenceEquals(ob, null))
                return false;
            Country c = ob as Country;
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
