using FlightCenterProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFlightCenterProject
{
    class TestResource
    {
        public const long FLIGHT_ID = 6;
        public const long AIRLINE_COMPANY_ID = 1;
        public const int VACANCY = 30;
        public const long ORIGIN_COUNTRY_CODE = 2;
        public const long DESTINATION_COUNTRY_CODE = 3;
        public static DateTime DEPARTURE_TIME = new DateTime(2019,07,29,13,0,0);
        public static DateTime LANDING_TIME = new DateTime(2019,07,29,21,0,0);
        public const string AIRLINE_NAME = "ELAL";
        public const string AIRLINE_USERNAME = "777";
        public const string AIRLINE_PASSWORD = "777";
        public const long COUNTRY_CODE = 1;
        public const string NEW_AIRLINE_PASSWORD = "666";
        public static List<long> airlinesId = new List<long>() { 1, 2 ,3,9};
        public static List<string> airlineNames = new List<string>() { "ELAL", "Alitalia" ,"AirEuropa", "AirEuropa" };
        public static List<long> airlineCountryCodes = new List<long>() { 3, 2,3 ,3};
        public const string CUSTOMER_USERNAME = "444";
        public const string CUSTOMER_PASSWORD = "444";
        public static List<long> FlightsId = new List<long>() { 6, 7 };
        public static List<long> AirlineCompaniesId = new List<long>() { 1, 2 };
        public static List<long> OriginCountryCodes = new List<long>() { 2, 3 };
        public static List<long> DestinationCountryCodes = new List<long>() { 3,2 };
        public static List<int> Vacancies = new List<int>() { 29, 20 };
        public static List<DateTime> DepartureTimes = new List<DateTime>() { new DateTime(2019, 07, 29, 13, 0, 0), new DateTime(2019, 08, 01, 08, 0, 0) };
        public static List<DateTime> LandingTimes = new List<DateTime>() { new DateTime(2019, 07, 29, 21, 0, 0), new DateTime(2019, 08, 01, 11, 0, 0) };
        public const string adminName = "admin";
        public const string adminPassWord = "9999";
        public static List<long> ticketsId = new List<long>() { 12, 13 };
        public static List<long> ticketFlightsId = new List<long>() { 38, 37 };
        public static List<long> ticketsCustomersId = new List<long>() { 2, 2 };

    }
}
