using FlightCenterProject;
using FlightWebApplication.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace UnitTestFlightCenterProject
{
    [TestClass]
   public class TestAdminFacade
    {
        [TestMethod]
        public void AdminFacade_CreateNewAirline_CompanyFound()
        {
            
            LoggedInAdministratorFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.adminName, TestResource.adminPassWord, out LoginTokenBase login) as LoggedInAdministratorFacade;
   
            AirlineCompany airline = new AirlineCompany("ELAL", "777", "777",1);

            AirlineCompany company = new AirlineCompany();

            company= facade.CreateNewAirline(login as LoginToken<Administrator>, airline);

            Assert.AreNotEqual(null, company);

            Assert.AreEqual(TestResource.AIRLINE_NAME, company.AirlineName);

            Assert.AreEqual(TestResource.AIRLINE_PASSWORD, company.Password);

            Assert.AreEqual(TestResource.AIRLINE_USERNAME, company.UserName);

            Assert.AreEqual(TestResource.COUNTRY_CODE,company.CountryCode);
          
        }

        [TestMethod]
        public void AirlineFacade_UpdateCustomerDetails()
        {

            LoggedInAdministratorFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.adminName, TestResource.adminPassWord, out LoginTokenBase login) as LoggedInAdministratorFacade;

            Customer customer= facade.GetCustomerById(login as LoginToken<Administrator>, 1);
            
            customer.FirstName = "Noam";

            facade.UpdateCustomerDetails(login as LoginToken<Administrator>, customer);

            Assert.AreEqual("Noam", customer.FirstName);

        }

        [TestMethod]
        public void AirlineFacade_CreateNewCustomer()
        {

            LoggedInAdministratorFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.adminName, TestResource.adminPassWord, out LoginTokenBase login) as LoggedInAdministratorFacade;

            facade.CreateNewCustomer(login as LoginToken<Administrator>, new Customer("Itamar", "Shemesh", "444", "444", "pt", "5555", "3232"));

        }

        [TestMethod]
        public void AirlineFacade_RemoveAirline()
        {

            LoggedInAdministratorFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.adminName, TestResource.adminPassWord, out LoginTokenBase login) as LoggedInAdministratorFacade;

            IList<AirlineCompany> airlines = facade.GetAllAirlineCompanies();

            facade.RemoveAirline(login as LoginToken<Administrator>, airlines[0]);

        }

        [TestMethod]
        public void AirlineFacade_RemoveCustomer()
        {

            LoggedInAdministratorFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.adminName, TestResource.adminPassWord, out LoginTokenBase login) as LoggedInAdministratorFacade;

            Customer customer = facade.GetCustomerById(login as LoginToken<Administrator>, 1);

            facade.RemoveCustomer(login as LoginToken<Administrator>, customer);

        }

        [TestMethod]
        public void AirlineFacade_UpdateAirlineDetails()
        {

            LoggedInAdministratorFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.adminName, TestResource.adminPassWord, out LoginTokenBase login) as LoggedInAdministratorFacade;

            IList<AirlineCompany> airlines = facade.GetAllAirlineCompanies();

            airlines[0].Password = "999";

            facade.UpdateAirlineDetails(login as LoginToken<Administrator>, airlines[0]);

        }

      
    }
}
