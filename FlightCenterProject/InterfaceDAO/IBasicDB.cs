using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public interface IBasicDB<T> where T:IPoco
    {
        T Get(long id);
        IList<T> GetAll();
        T Add(T t);
        void Remove(T t);
        void Update(T t);
        void RemoveAll();
    }
}
