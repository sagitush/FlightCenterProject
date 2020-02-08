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
   public class TicketDAOMSSQL : ITicketDAO
    {
        /// <summary>
        /// Adds a ticket to the data base.
        /// </summary>
        /// <param name="ticket"></param>
        public Ticket Add(Ticket ticket)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Tickets (FlightId,CustomerId)" +
                    $"VALUES({ticket.FlightId},{ticket.CustomerId})", conn);
                 
                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                return ticket;
            }
        }

        /// <summary>
        /// Gives a ticket by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ticket Get(long id)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Tickets WHERE Id={id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)

                {
                    Ticket ticket = new Ticket
                    {
                        Id = (long)reader["Id"],
                        FlightId = (long)reader["FlightId"],
                        CustomerId = (long)reader["CustomerId"],                      
                    };

                    cmd.Connection.Close();

                    return ticket;
                }
                else
                {
                    Debug.Write("there is no ticket with this id");

                    return null;
                }

            }
        }

        /// <summary>
        /// Gives a list of all the tickes.
        /// </summary>
        /// <returns></returns>
        public IList<Ticket> GetAll()
        {
            IList<Ticket> tickets = new List<Ticket>();

            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = new SqlConnection(FlightCenterConfig.dbName);

            cmd1.Connection.Open();

            cmd1.CommandType = CommandType.Text;

            cmd1.CommandText = $"SELECT * FROM Tickets";

            SqlDataReader reader = cmd1.ExecuteReader(CommandBehavior.Default);

            while (reader.Read() == true)
            {
                Ticket ticket = new Ticket
                {
                    Id = (long)reader["Id"],
                    FlightId = (long)reader["FlightId"],
                    CustomerId = (long)reader["CustomerId"],
                };

                tickets.Add(ticket);
            }
            cmd1.Connection.Close();

            return tickets;
        }

        /// <summary>
        /// Removes the ticket from the data base.
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Ticket t)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM Tickets WHERE Id ={t.Id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }

        /// <summary>
        /// Changes the datails of the ticket.
        /// </summary>
        /// <param name="t"></param>
        public void Update(Ticket t)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Customers SET FlightId={t.FlightId}," +
                   $"CustomerId={t.CustomerId} WHERE Id={t.Id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
        }

        public void RemoveAll()
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM Tickets", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }
    }
}
