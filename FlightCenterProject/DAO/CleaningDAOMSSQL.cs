using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
    public class CleaningDAOMSSQL:ICleaningDAO
    {
        /// <summary>
        /// Gets the flights that lands three hours ago
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetFlightsThatLandThreeHoursAgo()
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand("GET_FLIGHTS_THAT_LAND_THREE_HOURS_AGO", conn);

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
        /// Insert flights that land three hours ago to the Flight history table.
        /// </summary>
        public void InsertIntoFlightsHistory()
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand("INSERT_FLIGHTS_THAT_LAND_THREE_HOURS_AGO", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }

        /// <summary>
        /// Insert tickets of flights that land three hours ago to the Tickets history table.
        /// </summary>
        public void InsertIntoTicketsHistory(IList<Flight> flights)
        {
            foreach (Flight flight in flights)
            {
                long id = flight.Id;

                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
                {
                    SqlCommand cmd = new SqlCommand("INSERT_TICKETS_TO_HISTORY", conn);

                    cmd.Parameters.Add(new SqlParameter("@flightId", id));

                    cmd.Connection.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();

                }
            }
        }

        /// <summary>
        /// Deletes from Flights table the flights that land three hours ago
        /// </summary>
        /// <param name="flights"></param>
        public void DeleteFromFlights(IList<Flight> flights)
        {
            foreach (Flight flight in flights)
            {
                long id = flight.Id;

                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
                {
                    SqlCommand cmd = new SqlCommand("DELETE_FROM_FLIGHTS", conn);

                    cmd.Parameters.Add(new SqlParameter("@Id", id));

                    cmd.Connection.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();

                }

            }
        }

        /// <summary>
        /// Deletes from Tickets table the tickets of flights that land three hours ago
        /// </summary>
        /// <param name="flights"></param>
        public void DeleteFromTickets(IList<Flight> flights)
        {
            foreach (Flight flight in flights)
            {
                long id = flight.Id;

                using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
                {
                    SqlCommand cmd = new SqlCommand("DELETE_FROM_TICKETS", conn);

                    cmd.Parameters.Add(new SqlParameter("@flightId", id));

                    cmd.Connection.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();

                }

            }
        }
    }
}

