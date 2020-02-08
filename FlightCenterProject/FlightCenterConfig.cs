using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
    public static class FlightCenterConfig
    {
        public const string adminName = "admin";
        public const string adminPassWord = "9999";
        public const string dbName =/* @"Server = tcp:sagitdb.database.windows.net,1433;Initial Catalog = FlightCenter1; Persist Security Info=False;User ID = sagitdb; Password=Password1!; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";*/
             @"Data Source=LAPTOP-IMU119RQ\MSSQLSERVER02;Initial Catalog=FlightCenter;Integrated Security=True";
        public const int awakeTime = 24 * 60 * 60 * 1000;
    }
}
