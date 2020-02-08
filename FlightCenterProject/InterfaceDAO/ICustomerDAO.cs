using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public interface ICustomerDAO:IBasicDB<Customer>
    {
        Customer GetCustomerByUserName(string name);
        Ticket CheckIfCustomerHasCardForThisFlight(Flight flight,long id);
    }
}
