using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
    public class CleaningSystem:ICleaningSystem
    {       
        public void CleaningMetod()
        {
            CleaningDAOMSSQL cleaning = new CleaningDAOMSSQL();
            IList<Flight> flights = cleaning.GetFlightsThatLandThreeHoursAgo();
            cleaning.InsertIntoFlightsHistory();
            cleaning.InsertIntoTicketsHistory(flights);
            cleaning.DeleteFromTickets(flights);
            cleaning.DeleteFromFlights(flights);
        }
    }
}
