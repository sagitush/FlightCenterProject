using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
    public class FlightViewDAO : IFlightViewDAO
    {
       
        public IList<FlightView> GetFlightsThatLandAtTheNext12HoursAndLast4H()
        {
            IList<FlightView> flights = new List<FlightView>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {

                SqlCommand cmd = new SqlCommand("LANDING_FOR_THE_NEXT_12H_AND_LAST_4H", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    FlightView flight = new FlightView
                    {
                        FlightNumber = (long)reader["FLIGHT_NUMBER"],
                        AirlineCompanyName = (string)reader["AirlineName"],
                        OriginCountryName = (string)reader["COMING_FROM"],
                        DestinationCountryName = (string)reader["FLIGHT_TO"],
                        LandingTime = (DateTime)reader["LandingTime"],                        
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }

        public IList<FlightView> GetFlightsThatDepartureAtTheNext12Hours()
        {
            IList<FlightView> flights = new List<FlightView>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {

                SqlCommand cmd = new SqlCommand("DEPARTURE_FOR_THE_NEXT_12H", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    FlightView flight = new FlightView
                    {
                        FlightNumber = (long)reader["FLIGHT_NUMBER"],
                        AirlineCompanyName = (string)reader["AirlineName"],
                        OriginCountryName = (string)reader["COMING_FROM"],
                        DestinationCountryName = (string)reader["FLIGHT_TO"],
                        LandingTime = (DateTime)reader["DepartureTime"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }

        public IList<FlightView> GetLandingAndDepartureFlightView()
        {
            IList<FlightView> flights = new List<FlightView>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {

                SqlCommand cmd = new SqlCommand("GET_DEPARTURE_AND_LANDING_FLIGHTS", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    FlightView flight = new FlightView
                    {
                        FlightNumber = (long)reader["FLIGHT_NUMBER"],
                        AirlineCompanyName = (string)reader["AirlineName"],
                        OriginCountryName = (string)reader["COMING_FROM"],
                        DestinationCountryName = (string)reader["FLIGHT_TO"],
                        DepartureTime=(DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }

        public IList<FlightView> GetFlightsViewByFilter(int flightNum, string originCoun, string destCoun,string airline, string searchType )
        {
            IList<FlightView> flights = new List<FlightView>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {

                SqlCommand cmd = new SqlCommand("GET_FLIGHT_BY_FILTER", conn);

                cmd.Parameters.Add(new SqlParameter("@FlightNum", flightNum));
                cmd.Parameters.Add(new SqlParameter("@OriginContryName", originCoun));
                cmd.Parameters.Add(new SqlParameter("@DestinatonCountryName", destCoun));
                cmd.Parameters.Add(new SqlParameter("@AirlineCompanyName", airline));
                cmd.Parameters.Add(new SqlParameter("@SearchType", searchType));
              
                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    FlightView flight = new FlightView
                    {
                        FlightNumber = (long)reader["FLIGHT_NUMBER"],
                        AirlineCompanyName = (string)reader["AirlineName"],
                        OriginCountryName = (string)reader["COMING_FROM"],
                        DestinationCountryName = (string)reader["FLIGHT_TO"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }
    }
}
