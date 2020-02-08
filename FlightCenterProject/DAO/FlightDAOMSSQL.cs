using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class FlightDAOMSSQL : IFlightDAO
    {
        /// <summary>
        /// Adds a flight to the data base.
        /// </summary>
        /// <param name="flight"></param>
        public Flight Add(Flight flight)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand("ADD_Flight", conn);

                cmd.Parameters.Add(new SqlParameter("@AirlineCompanyId", flight.AirlineCompanyId));
                cmd.Parameters.Add(new SqlParameter("@OriginCountryCode", flight.OriginCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DestinationCountryCode", flight.DestinationCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DepartureTime", flight.DepartureTime));
                cmd.Parameters.Add(new SqlParameter("@LandingTime", flight.LandingTime));
                cmd.Parameters.Add(new SqlParameter("@RemainingTickets", flight.RemainingTickets));

                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
               
                return flight;
            }
        }
        /// <summary>
        ///  Gives flight by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flight Get(long id)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Flights WHERE Id={id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)

                {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    cmd.Connection.Close();

                    return flight;
                }
                else
                {
                    Debug.Write("there is no flight with this id");

                    return null;
                }

            }
        }

        /// <summary>
        /// Gives a list of all the flights.
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAll()
        {
            IList<Flight> flights = new List<Flight>();

            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = new SqlConnection(FlightCenterConfig.dbName);

            cmd1.Connection.Open();

            cmd1.CommandType = CommandType.Text;

            cmd1.CommandText = $"SELECT * FROM Flights";

            SqlDataReader reader = cmd1.ExecuteReader(CommandBehavior.Default);

            while (reader.Read() == true)
            {
                Flight flight = new Flight
                {
                    Id = (long)reader["Id"],
                    AirlineCompanyId = (long)reader["AirLineCompanyId"],
                    OriginCountryCode = (long)reader["OriginCountryCode"],
                    DestinationCountryCode = (long)reader["DestinationCountryCode"],
                    DepartureTime = (DateTime)reader["DepartureTime"],
                    LandingTime = (DateTime)reader["LandingTime"],
                    RemainingTickets = (int)reader["RemainingTickets"],
                };

                flights.Add(flight);
            }
            cmd1.Connection.Close();

            return flights;
        }

        /// <summary>
        /// Gives a dictionary of all flights and their vacancy. 
        /// </summary>
        /// <returns></returns>
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flightsVacancy = new Dictionary<Flight, int>();

            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = new SqlConnection(FlightCenterConfig.dbName);

            cmd1.Connection.Open();

            cmd1.CommandType = CommandType.Text;

            cmd1.CommandText = $"SELECT * FROM Flights";

            SqlDataReader reader = cmd1.ExecuteReader(CommandBehavior.Default);

            while (reader.Read() == true)
            {
                Flight flight = new Flight
                {
                    Id = (long)reader["Id"],
                    AirlineCompanyId = (long)reader["AirLineCompanyId"],
                    OriginCountryCode = (long)reader["OriginCountryCode"],
                    DestinationCountryCode = (long)reader["DestinationCountryCode"],
                    DepartureTime = (DateTime)reader["DepartureTime"],
                    LandingTime = (DateTime)reader["LandingTime"],
                    RemainingTickets = (int)reader["RemainingTickets"],
                };

                flightsVacancy.Add(flight, flight.RemainingTickets);
            }
            cmd1.Connection.Close();

            return flightsVacancy;

        }

        /// <summary>
        /// Gives flight by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flight GetFlightById(long id)
        {

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Flights WHERE Id={id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)

                {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    cmd.Connection.Close();

                    return flight;
                }
                else
                {
                    Debug.Write("there is no flight with this id");

                    return null;
                }
            }
        }

        /// <summary>
        /// Gives a list of all flights by customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT F.Id, F.AirLineCompanyId,F.OriginCountryCode" +
                    $",F.DestinationCountryCode,F.DepartureTime,F.LandingTime,F.RemainingTickets" +
                    $" from Flights F JOIN Tickets T on F.Id = T.FlightId JOIN Customers C on T.CustomerId = {customer.Id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }

        /// <summary>
        /// Gives a list of flights by departure date.
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_DEPATRTUREDATE", conn);

                cmd.Parameters.Add(new SqlParameter("@DepartureTime", departureDate));

                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }

        /// <summary>
        /// Gives a list of flights by destination country.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Flights WHERE DestinationCountryCode={countryCode}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }

        /// <summary>
        /// Gives a list of flights by landing date.
        /// </summary>
        /// <param name="landingDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                               
                    SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_LANDINGDATE", conn);

                    cmd.Parameters.Add(new SqlParameter("@LandingTime",landingDate));

                    cmd.Connection.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                   while(reader.Read() == true)

                  {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }

        /// <summary>
        /// Gives a list of flights by origin country.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByOriginCountry(long countryCode)
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Flights WHERE OriginCountryCode={countryCode}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }

        /// <summary>
        /// Removes the flight from the data base.
        /// </summary>
        /// <param name="flight"></param>
        public void Remove(Flight flight)
        {
            
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                
                 SqlCommand cmd1 = new SqlCommand("DELETE_FROM_TICKETS", conn);

                 cmd1.Parameters.Add(new SqlParameter("@flightId", flight.Id));

                 cmd1.Connection.Open();

                 cmd1.CommandType = CommandType.StoredProcedure;

                 cmd1.ExecuteNonQuery();

                 cmd1.Connection.Close();

                 SqlCommand cmd = new SqlCommand($"DELETE FROM Flights WHERE Id ={flight.Id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }

       /// <summary>
       /// Changes the details of the flight.
       /// </summary>
      /// <param name="flight"></param>

        public void Update(Flight flight)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd1 = new SqlCommand("UPDATE_FLIGHT", conn);

                cmd1.Parameters.Add(new SqlParameter("@Id" , flight.Id));
                cmd1.Parameters.Add(new SqlParameter("@AirlineCompanyId", flight.AirlineCompanyId));
                cmd1.Parameters.Add(new SqlParameter("@OriginCountryCode", flight.OriginCountryCode));
                cmd1.Parameters.Add(new SqlParameter("@DestinationCountryCode", flight.DestinationCountryCode));
                cmd1.Parameters.Add(new SqlParameter("@DepartureTime", flight.DepartureTime));
                cmd1.Parameters.Add(new SqlParameter("@LandingTime", flight.LandingTime));
                cmd1.Parameters.Add(new SqlParameter("@RemainingTickets", flight.RemainingTickets));

                cmd1.Connection.Open();

                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.ExecuteNonQuery();

                cmd1.Connection.Close();
                
            }

        }

        public void RemoveAll()
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd1 = new SqlCommand($"DELETE FROM Tickets", conn);

                cmd1.Connection.Open();

                cmd1.CommandType = CommandType.Text;

                cmd1.ExecuteNonQuery();

                cmd1.Connection.Close();

                SqlCommand cmd = new SqlCommand($"DELETE FROM Flights", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }

        public IList<Flight> GetFlightsThatDepartureAtTheNext12Hours()
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {

                SqlCommand cmd = new SqlCommand("GET_FLIGHTS_DEPARTURE_AT_12_HOURS", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }
        }

        public IList<Flight> GetFlightsThatLandAtTheNext12Hours()
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {

                SqlCommand cmd = new SqlCommand("GET_FLIGHTS_LAND_12_HOURS", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    flights.Add(flight);
                }
                cmd.Connection.Close();

                return flights;

            }

        }

        public IList<Flight> GetFoodsByFilter(long originCountry, long destinationCountry, long airlineName, long flightNumber)
        {

            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd1 = new SqlCommand("GET_FLIGHTS_BY_FILTER", conn);

                cmd1.Parameters.Add(new SqlParameter("@FlightNum", flightNumber));
                cmd1.Parameters.Add(new SqlParameter("@OriginContry", originCountry));
                cmd1.Parameters.Add(new SqlParameter("@DestinatonCountry", destinationCountry));
                cmd1.Parameters.Add(new SqlParameter("@AirlineCompany", airlineName));
              
                cmd1.Connection.Open();

                cmd1.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd1.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)

                {
                    Flight flight = new Flight
                    {
                        Id = (long)reader["Id"],
                        AirlineCompanyId = (long)reader["AirLineCompanyId"],
                        OriginCountryCode = (long)reader["OriginCountryCode"],
                        DestinationCountryCode = (long)reader["DestinationCountryCode"],
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        LandingTime = (DateTime)reader["LandingTime"],
                        RemainingTickets = (int)reader["RemainingTickets"],
                    };

                    flights.Add(flight);
                }
                cmd1.Connection.Close();

                return flights;
            }
        }

    }
}
