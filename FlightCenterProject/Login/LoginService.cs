using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class LoginService : ILoginService
    {
        private AirlineCompanyDAOMSSQL _airlineCompanyDAO;

        private CustomerDAOMSSQL _custonerDAO;

        public LoginService()
        {
            _airlineCompanyDAO = new AirlineCompanyDAOMSSQL();
            _custonerDAO = new CustomerDAOMSSQL();
        }

        /// <summary>
        /// Try the username and password for administrator login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token)
        {
            if (userName== FlightCenterConfig.adminName && password == FlightCenterConfig.adminPassWord)
            {
                token = new LoginToken<Administrator>();
                token.User = new Administrator();
                return true;
            }

            token = null;
            return false;
        }

        /// <summary>
        /// Try the username and password for airline company login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool TryArilineLogin(string userName, string password, out LoginToken<AirlineCompany> token)
        {
            AirlineCompany company = _airlineCompanyDAO.GetAirlineByUserName(userName);
            if (company != null)
            {
                try
                {
                    if (company.Password == password)
                    {
                        token = new LoginToken<AirlineCompany>() { User = company };
                        return true;
                    }
                    else
                    {
                        throw new WrongPasswordException($"airline company{company.AirlineName} tried wrong password");
                    }
                }
                catch (WrongPasswordException w)
                {
                    Debug.Write(w.Message);
                }                                        
            }
            token = null;
            return false;
        }

        /// <summary>
        /// Try the username and password for customer login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token)
        {
            Customer customer = _custonerDAO.GetCustomerByUserName(userName);
            if(customer!=null)
            {
                try
                {
                    if (customer.Password == password)
                    {
                        token = new LoginToken<Customer>() { User = customer };
                        return true;
                    }
                    else
                    {
                        throw new WrongPasswordException($"customer with id:{customer.Id} tried wrong password");
                    }
                }catch(WrongPasswordException w)
                {
                    Debug.Write(w.Message);
                }
            }
            token = null;
            return false;
        }
    }
}
