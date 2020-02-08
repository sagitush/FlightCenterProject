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
  public class CountryDAOMSSQL : ICountryDAO
    {
        /// <summary>
        /// Add a country to the data base.
        /// </summary>
        /// <param name="country"></param>
        public Country Add(Country country)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Countries (CountryName) VALUES ('{country.CountryName}')", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                return country;
            }
        }

        /// <summary>
        /// Gives a country by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Country Get(long id)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Countries WHERE Id={id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)

                {
                    Country country = new Country
                    {
                        Id = (long)reader["Id"],
                        CountryName = (string)reader["CountryName"]
                    };

                    cmd.Connection.Close();

                    return country;
                }
                else
                {
                    Debug.Write("there is no country with this id");

                    return null;
                }

            }
        }

        /// <summary>
        /// Gives a list of all the countries.
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAll()
        {
            IList<Country> countries = new List<Country>();

            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = new SqlConnection(FlightCenterConfig.dbName);

            cmd1.Connection.Open();

            cmd1.CommandType = CommandType.Text;

            cmd1.CommandText = $"SELECT * FROM Countries";

            SqlDataReader reader = cmd1.ExecuteReader(CommandBehavior.Default);

            while (reader.Read() == true)
            {
                Country country = new Country
                {
                    Id = (long)reader["Id"],
                    CountryName = (string)reader["CountryName"]
                };


                countries.Add(country);
            }
            cmd1.Connection.Close();

            return countries;
        }

        /// <summary>
        ///Removes a country from the data base(only if the country doesnt exist in the other tables)
        /// </summary>
        /// <param name="country"></param>

        public void Remove(Country country)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM Countries WHERE Id ={country.Id}", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }

        /// <summary>
        /// Changes the details of the country.
        /// </summary>
        /// <param name="country"></param>
        public void Update(Country country)
        {
            using (SqlConnection conn = new SqlConnection(FlightCenterConfig.dbName))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Countries SET CountryName='{country.CountryName}'," +
                   $"WHERE Id={country.Id}", conn);

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
                SqlCommand cmd = new SqlCommand($"DELETE FROM Countries", conn);

                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

            }
        }
    }
}
