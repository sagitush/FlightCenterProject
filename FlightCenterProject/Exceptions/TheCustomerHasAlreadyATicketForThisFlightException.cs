using System;
using System.Runtime.Serialization;

namespace FlightCenterProject
{
    [Serializable]
    internal class TheCustomerHasAlreadyATicketForThisFlightException : Exception
    {
        public TheCustomerHasAlreadyATicketForThisFlightException()
        {
        }

        public TheCustomerHasAlreadyATicketForThisFlightException(string message) : base(message)
        {
        }

        public TheCustomerHasAlreadyATicketForThisFlightException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TheCustomerHasAlreadyATicketForThisFlightException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}