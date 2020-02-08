using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public interface ILoggedInAdministratorFacade
    {
        AirlineCompany CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline);

        void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline);

        void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline);

        void CreateNewCustomer(LoginToken<Administrator> token, Customer customer);

        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);

        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);

        Customer GetCustomerById(LoginToken<Administrator> token, long id);

        bool CheckUserNameAndPwdAirline(LoginToken<Administrator> token, string username, string password);

        AirlineCompany GetAirlineByUsername(LoginToken<Administrator> token, string username);

        bool CheckUserNameAndPwdCustomer(LoginToken<Administrator> token, string username, string password);

        Customer GetCustomerByUsername(LoginToken<Administrator> token, string username);
    }
}
