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
   public class AirlineCompanyDAOMSSQL : IAirlineDAO
    {
        /// <summary>
        /// Adds a airline company to the data base.
        /// </summary>
        /// <param name="company"></param>
        public AirlineCompany Add(AirlineCompany company)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO AirlineCompany (AirlineName,UserName,Password,CountryCode)" +
                    $"VALUES('{company.AirlineName}','{company.UserName}','{company.Password}',{company.CountryCode})", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                return company;
            }
        }

        /// <summary>
        /// Gives a airline company by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AirlineCompany Get(long id)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM AirlineCompany WHERE Id={id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)

                {
                    AirlineCompany company = new AirlineCompany
                    {
                        Id = (long)reader["Id"],
                        AirlineName = (string)reader["AirlineName"],
                        UserName = (string)reader["UserName"],
                        Password = (string)reader["Password"],
                        CountryCode = (long)reader["CountryCode"]
                    };

                    cmd.Connection.Close();

                    return company;
                }
                else
                {
                    Debug.Write("there is no airline company with this id");

                    return null;
                }

            }
        }

        /// <summary>
        /// Gives a airline company by user name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AirlineCompany GetAirlineByUserName(string name)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM AirlineCompany WHERE UserName='{name}'", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)

                {
                    AirlineCompany company = new AirlineCompany
                    {
                        Id = (long)reader["Id"],
                        AirlineName = (string)reader["AirlineName"],
                        UserName = (string)reader["UserName"],
                        Password = (string)reader["Password"],
                        CountryCode = (long)reader["CountryCode"]
                    };

                    cmd.Connection.Close();

                    return company;
                }
                else
                {
                    Debug.Write("there is no airline company with this name");

                    return null;
                }

            }

        }

        /// <summary>
        /// Gives a list of the all airline companies.
        /// </summary>
        /// <returns></returns>
        public IList<AirlineCompany> GetAll()
        {
            IList<AirlineCompany> companies = new List<AirlineCompany>();

            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = new SqlConnection(FlightCenterConfig.dbName);

            cmd1.Connection.Open();

            cmd1.CommandType = CommandType.Text;

            cmd1.CommandText = $"SELECT * FROM AirlineCompany";

            SqlDataReader reader = cmd1.ExecuteReader(CommandBehavior.Default);

            while (reader.Read() == true)
            {
                AirlineCompany company = new AirlineCompany
                {
                    Id = (long)reader["Id"],
                    AirlineName = (string)reader["AirlineName"],
                    UserName = (string)reader["UserName"],
                    Password = (string)reader["Password"],
                    CountryCode = (long)reader["CountryCode"]
                };

                companies.Add(company);
            }
            cmd1.Connection.Close();

            return companies;
        }

        /// <summary>
        /// Gives a list of airline companies from the same country.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlinesByCountry(long countryId)
        {
            IList<AirlineCompany> companies = new List<AirlineCompany>();

            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = new SqlConnection(FlightCenterConfig.dbName);

            cmd1.Connection.Open();

            cmd1.CommandType = CommandType.Text;

            cmd1.CommandText = $"SELECT * FROM AirlineCompany WHERE CountryCode={countryId}";

            SqlDataReader reader = cmd1.ExecuteReader(CommandBehavior.Default);

            while (reader.Read() == true)
            {
                AirlineCompany company = new AirlineCompany
                {
                    Id = (long)reader["Id"],
                    AirlineName = (string)reader["AirlineName"],
                    UserName = (string)reader["UserName"],
                    Password = (string)reader["Password"],
                    CountryCode = (long)reader["CountryCode"]
                };

                companies.Add(company);
            }

            cmd1.Connection.Close();

            return companies;
        }

        /// <summary>
        /// Remove the airline company from the database.
        /// </summary>
        /// <param name="company"></param>
        public void Remove(AirlineCompany company)
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd1 = new SqlCommand($"SELECT * FROM Flights WHERE AirLineCompanyId ={company.Id}", conn);

                cmd1.Connection.Open();

                cmd1.CommandType = CommandType.Text;

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

                foreach (Flight flight in flights)
                {
                    long id = flight.Id;

                    SqlCommand cmd2 = new SqlCommand("DELETE_FROM_TICKETS", conn);

                    cmd2.Parameters.Add(new SqlParameter("@flightId", id));

                    cmd2.Connection.Open();

                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.ExecuteNonQuery();

                    cmd2.Connection.Close();

                }

                SqlCommand cmd3 = new SqlCommand($"DELETE FROM Flights WHERE AirLineCompanyId ={company.Id}", conn);

                cmd3.Connection.Open();

                cmd3.CommandType = CommandType.Text;

                cmd3.ExecuteNonQuery();

                cmd3.Connection.Close();


                SqlCommand cmd = new SqlCommand($"DELETE FROM AirlineCompany WHERE Id ={company.Id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }

        /// <summary>
        /// Changes the details of the airline company.
        /// </summary>
        /// <param name="company"></param>
        public void Update(AirlineCompany company)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE AirlineCompany SET AirlineName='{company.AirlineName}'," +
                   $" UserName='{company.UserName}',Password='{company.Password}',CountryCode={company.CountryCode}" +
                   $"WHERE Id={company.Id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }

        /// <summary>
        /// Gives a list of all tickets of a specific airline.
        /// </summary>
        /// <param name="airlineId"></param>
        /// <returns></returns>
        public IList<Ticket> GetTicketsOfTheAirline(long airlineId)
        {
            IList<Ticket> tickets = new List<Ticket>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_TICKETS_OF_THE_AIRLINE", conn);

                cmd.Parameters.Add(new SqlParameter("@AirlineId", airlineId));

                cmd.Connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {
                    Ticket ticket = new Ticket
                    {
                        Id = (long)reader["TIKTS_ID"],
                        FlightId = (long)reader["FL_ID"],
                        CustomerId = (long)reader["CustomerId"],
                    };

                    tickets.Add(ticket);
                }

                cmd.Connection.Close();

            }

            return tickets;
        }

        /// <summary>
        /// Gives a list of all flights of a specific airline.
        /// </summary>
        /// <param name="airlineId"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsOfTheAirline(long airlineId)
        {
            IList<Flight> flights = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_FLIGHTS_OF_THE_AIRLINE", conn);

                cmd.Parameters.Add(new SqlParameter("@AirlineId", airlineId));

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
            }

            return flights;
        }

        /// <summary>
        /// Gives a airline company by password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public AirlineCompany GetAirlineByPassword(string password)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM AirlineCompany WHERE UserName='{password}'", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)

                {
                    AirlineCompany company = new AirlineCompany
                    {
                        Id = (long)reader["Id"],
                        AirlineName = (string)reader["AirlineName"],
                        UserName = (string)reader["UserName"],
                        Password = (string)reader["Password"],
                        CountryCode = (long)reader["CountryCode"]
                    };

                    cmd.Connection.Close();

                    return company;
                }
                else
                {
                    Debug.Write("there is no airline company with this password");

                    return null;
                }
            }
        }

        public void RemoveAll()
        {
 
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                
                SqlCommand cmd3 = new SqlCommand($"DELETE FROM Flights", conn);

                cmd3.Connection.Open();

                cmd3.CommandType = CommandType.Text;

                cmd3.ExecuteNonQuery();

                cmd3.Connection.Close();


                SqlCommand cmd = new SqlCommand($"DELETE FROM AirlineCompany", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }

      


    }
}    
