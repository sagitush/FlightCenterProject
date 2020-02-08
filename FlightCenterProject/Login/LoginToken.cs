using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class LoginToken<T>:LoginTokenBase where T:IUser
    {
        public T User { get; set; }
    }
}
