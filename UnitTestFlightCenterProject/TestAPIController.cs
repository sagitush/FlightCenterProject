using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.Http;
using System.Net.Http;
using FlightWebApplication.Controllers;
using FlightCenterProject;
using System.Net.Http.Headers;

namespace UnitTestFlightCenterProject
{
    [TestClass]
    public class TestAPIController
    {
        
        private const string URL = "http://localhost:59701";
        private string GetAllCompaniesUrl = $"{URL}/api/anonymousFacade/allAirlines";
        private string CreateNewAirlineUrl = $"{URL}/api/AdministratorFacade/CreateNewAirline";

        [TestMethod]
        public void TestForAnonymousController()
        {
            LoggedInAdministratorFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.adminName, TestResource.adminPassWord, out LoginTokenBase login) as LoggedInAdministratorFacade;
            AirlineCompany company = new AirlineCompany("sagitair", "999", "888", 1);
            facade.CreateNewAirline(login as LoginToken<Administrator>, company);
            List<AirlineCompany> companies = GetAllCompanies(GetAllCompaniesUrl);
            Assert.IsTrue(companies.Contains(company));
        }

        [TestMethod]
        public void TestForAdminController()
        {
            LoggedInAdministratorFacade facade = FlyingCenterSystem.GetInstance().Login(TestResource.adminName, TestResource.adminPassWord, out LoginTokenBase login) as LoggedInAdministratorFacade;
            AirlineCompany company = new AirlineCompany("sagitair", "999", "888", 1);
            facade.CreateNewAirline(login as LoginToken<Administrator>, company);
            List<AirlineCompany> companies = GetAllCompanies(GetAllCompaniesUrl);
            Assert.IsTrue(companies.Contains(company));
        }

        private List<AirlineCompany> GetAllCompanies(string url)
        {
            List<AirlineCompany> companies = new List<AirlineCompany>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        companies = content.ReadAsAsync<List<AirlineCompany>>().Result;
                    }
                }
            }
            return companies;
        }

        private string GetTokenAsync(string name, string password)
        {
            string token = "";
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            client.BaseAddress = new Uri(GetTokenUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string userDetails =
                "{" +
                $"\"User_Name\":  \"{name}\"," +
                $" \"Password\": \"{password}\"" +
                "}";
            HttpContent httpContent = new StringContent(userDetails, Encoding.UTF8, "application/json");
            response = client.PostAsync(GetTokenUrl, httpContent).Result;
            token = response.Content.ReadAsStringAsync().Result;
            return FixApiResponseString(token);
        }

    }

  
}
