using System.Collections.Generic;

namespace FlightCenterProject
{
    public interface ICleaningDAO
    {
        IList<Flight> GetFlightsThatLandThreeHoursAgo();
        void InsertIntoFlightsHistory();
        void InsertIntoTicketsHistory(IList<Flight> flights);
        void DeleteFromFlights(IList<Flight> flights);
        void DeleteFromTickets(IList<Flight> flights);
    }
}