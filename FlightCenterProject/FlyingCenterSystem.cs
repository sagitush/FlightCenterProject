using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightCenterProject
{
    public class FlyingCenterSystem
    {
        private static FlyingCenterSystem _instance;

        private static object key = new object();
        
        private FlyingCenterSystem()
        {
            System.Timers.Timer timer = new System.Timers.Timer(FlightCenterConfig.awakeTime);
            timer.Elapsed += Cleaning_Timer;
            timer.Start();
        }
         
        private void Cleaning_Timer(object sender, System.Timers.ElapsedEventArgs e)
        {
            CleaningSystem cleaningSystem = new CleaningSystem();
            cleaningSystem.CleaningMetod();
        }

        /// <summary>
        /// Try to login with username and password, set the token and returs match facade. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public FacadeBase Login(string username, string password, out LoginTokenBase login)
        {
            LoginService service = new LoginService();
            LoginToken<Administrator> tokenAdmin;
            LoginToken<AirlineCompany> tokenAir;
            LoginToken<Customer> tokenCust;

            if (service.TryAdminLogin(username, password, out tokenAdmin))
            {
                login = tokenAdmin;
                if (login != null)
                {
                    FacadeBase adminFacade = new LoggedInAdministratorFacade();
                    return adminFacade;
                }
            }
            else
                 if (service.TryArilineLogin(username, password, out tokenAir))
            {
                login = tokenAir;
                if (login != null)
                {
                    FacadeBase airlineFacade = new LoggedInAirlineFacade();
                    return airlineFacade;
                }
            }
            else
                 if (service.TryCustomerLogin(username, password, out tokenCust))
            {
                login = tokenCust;
                if (login != null)
                {
                    FacadeBase custFacade = new LoggedInCustomerFacade();
                    return custFacade;
                }
            }
            else
            
                login = null;
                return null;            
        }

        /// <summary>
        /// login anonymous user and return anonymous facade.
        /// </summary>
        /// <returns></returns>
        public AnonymousUserFacade GetAnonymousFacade()
        {
            AnonymousUserFacade anonymFacade = new AnonymousUserFacade();
            return anonymFacade;
        }
       
        public static FlyingCenterSystem GetInstance()
        {
            if (_instance == null)
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new FlyingCenterSystem();
                    }
                }
            }
            return _instance;
        }
    }
}
