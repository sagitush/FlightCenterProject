using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
    public interface IFlightViewDAO
    {
        IList<FlightView> GetFlightsThatLandAtTheNext12HoursAndLast4H();
        IList<FlightView> GetFlightsThatDepartureAtTheNext12Hours();
        IList<FlightView> GetLandingAndDepartureFlightView();
        IList<FlightView> GetFlightsViewByFilter(int flightNum, string originCoun, string destCoun, string airline, string searchType);
       
    }
}
