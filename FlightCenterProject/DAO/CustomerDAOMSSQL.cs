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
   public class CustomerDAOMSSQL:ICustomerDAO
    {
        /// <summary>
        /// Adds a customer to the data base.
        /// </summary>
        /// <param name="customer"></param>
        public Customer Add(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Customers (FirstName,LastName,UserName,Password,Address," +
                    $"PhoneNo,CreditCardNumber) VALUES('{customer.FirstName}','{customer.LastName}','{customer.UserName}',"+
                    $"'{customer.Password}','{customer.Address}','{customer.PhoneNo}','{customer.CreditCardNumber}')", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                return customer;
            }
        }

        /// <summary>
        /// Checks if this customer has card for this flight
        /// </summary>
        /// <param name="flight"></param>
        public Ticket CheckIfCustomerHasCardForThisFlight(Flight flight,long id)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Tickets WHERE CustomerId={id} and flightId={flight.Id}", conn);

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
                    return null;
            
            }
               
        }

        /// <summary>
        /// Gives a customer by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer Get(long id)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Customers WHERE Id={id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)

                {
                    Customer customer = new Customer
                    {
                        Id = (long)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        UserName=(string)reader["UserName"],
                        Password = (string)reader["Password"],
                        Address = (string)reader["Address"],
                        PhoneNo=(string)reader["PhoneNo"],
                        CreditCardNumber = (string)reader["CreditCardNumber"]
                    };

                    cmd.Connection.Close();

                    return customer;
                }
                else
                {
                    Debug.Write("there is no customer with this id");

                    return null;
                }

            }
        }

        /// <summary>
        /// Gives a list of all the customers. 
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetAll()
        {
            IList<Customer> customers = new List<Customer>();

            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = new SqlConnection(FlightCenterConfig.dbName);

            cmd1.Connection.Open();

            cmd1.CommandType = CommandType.Text;

            cmd1.CommandText = $"SELECT * FROM Customers";

            SqlDataReader reader = cmd1.ExecuteReader(CommandBehavior.Default);

            while (reader.Read() == true)
            {
                Customer customer = new Customer
                {
                    Id = (long)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    UserName = (string)reader["UserName"],
                    Password = (string)reader["Password"],
                    Address = (string)reader["Address"],
                    PhoneNo = (string)reader["PhoneNo"],
                    CreditCardNumber = (string)reader["CreditCardNumber"]
                };

                customers.Add(customer);
            }
            cmd1.Connection.Close();

            return customers;
        }

        /// <summary>
        /// Gives a customer by user name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Customer GetCustomerByUserName(string name)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Customers WHERE UserName='{name}'", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)

                {
                    Customer customer = new Customer
                    {
                        Id = (long)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        UserName = (string)reader["UserName"],
                        Password = (string)reader["Password"],
                        Address = (string)reader["Address"],
                        PhoneNo = (string)reader["PhoneNo"],
                        CreditCardNumber = (string)reader["CreditCardNumber"]
                    };

                    cmd.Connection.Close();

                    return customer;
                }
                else
                {
                    Debug.Write("there is no airline company with this name");

                    return null;
                }

            }
        }

        /// <summary>
        /// Removes the customer from the data base.
        /// </summary>
        /// <param name="customer"></param>
        public void Remove(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd1 = new SqlCommand($"DELETE FROM Tickets WHERE CustomerId ={customer.Id}", conn);

                cmd1.Connection.Open();

                cmd1.CommandType = CommandType.Text;

                cmd1.ExecuteNonQuery();

                cmd1.Connection.Close();

                SqlCommand cmd = new SqlCommand($"DELETE FROM Customers WHERE Id ={customer.Id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }

        /// <summary>
        /// Changes the details of the customer.
        /// </summary>
        /// <param name="customer"></param>
        public void Update(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Customers SET FirstName='{customer.FirstName}'," +
                   $"LastName='{customer.LastName}',UserName='{customer.UserName}',Password='{customer.Password}'," +
                   $"Address='{customer.Address}',PhoneNo='{customer.PhoneNo}',CreditCardNumber='{customer.CreditCardNumber}'" +
                   $"WHERE Id={customer.Id}", conn);

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
                SqlCommand cmd1 = new SqlCommand($"DELETE FROM Tickets", conn);

                cmd1.Connection.Open();

                cmd1.CommandType = CommandType.Text;

                cmd1.ExecuteNonQuery();

                cmd1.Connection.Close();

                SqlCommand cmd = new SqlCommand($"DELETE FROM Customers", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }



    }
}
