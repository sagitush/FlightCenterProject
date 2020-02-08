using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightCenterProject;
using FlightCenterProject.Facade;

namespace FlightWebApplication.Controllers
{
    [BasicAuthenticationAdministrator]
    public class AdministratorFacadeController : ApiController
    {
        static ApiFacade apiFacade;
        LoginToken<Administrator> adminLoginToken;

        static AdministratorFacadeController()
        {
            apiFacade = new ApiFacade();
        }

        public LoggedInAdministratorFacade GetAdminFacade()
        {
            Request.Properties.TryGetValue("administratorToken", out object loginUser);
            adminLoginToken = (LoginToken<Administrator>)loginUser;
            Request.Properties.TryGetValue("administratorFacade", out object facade);
            
            return (LoggedInAdministratorFacade)facade;
        }


        [HttpPost]
        [Route("api/AdministratorFacade/CreateNewAirline")]
        public IHttpActionResult CreateNewAirline([FromBody]AirlineCompany airlineCompany)
        {
            if(airlineCompany == null)
                return BadRequest(ModelState);
            else
            {
                GetAdminFacade().CreateNewAirline(adminLoginToken, airlineCompany);
                return Ok();
            }
        }
        
        [HttpPost]
        [Route("api/AdministratorFacade/CreateNewCustomer")]
        public IHttpActionResult CreateNewCustomer([FromBody]Customer customer)
        {           
            if (customer == null)
                return BadRequest(ModelState);
            GetAdminFacade().CreateNewCustomer(adminLoginToken, customer);
            return Ok();

        }

        [HttpPut]
        [Route("api/AdministratorFacade/UpdateAirlineDetails/{id}")]
        public IHttpActionResult UpdateAirlineDetails([FromBody]AirlineCompany airline, [FromUri]long id)
        {

            if (apiFacade.GetMyCompany(id) == null)
                return BadRequest(ModelState);
            else
            {
                airline.Id = id;
                GetAdminFacade().UpdateAirlineDetails(adminLoginToken, airline);
                return Ok();
            }
        }

        [HttpPut]
        [Route("api/AdministratorFacade/UpdateCustomerDetails/{id}")]
        public IHttpActionResult UpdateCustomerDetails([FromBody]Customer customer, [FromUri]long id)
        {
                
            if (apiFacade.GetMyCustomer(id) == null)
                return BadRequest(ModelState);
            else
            {
                customer.Id = id;
                GetAdminFacade().UpdateCustomerDetails(adminLoginToken, customer);
                return Ok();
            }
            
        }

        [HttpDelete]
        [Route("api/AdministratorFacade/RemoveAirline")]
        public IHttpActionResult RemoveAirline(long companyId)
        {
            AirlineCompany airline = apiFacade.GetMyCompany(companyId);
            if (airline == null)
                return BadRequest(ModelState);
            GetAdminFacade().RemoveAirline(adminLoginToken, airline);
            return Ok();
        }

        [HttpDelete]
        [Route("api/AdministratorFacade/RemoveCustomer")]
        public IHttpActionResult RemoveCustomer(long customerId)
        {
            Customer customer = apiFacade.GetMyCustomer(customerId);
            if (customer == null)
                return BadRequest(ModelState);
            GetAdminFacade().RemoveCustomer(adminLoginToken, customer);
            return Ok();
        }
    }
}
