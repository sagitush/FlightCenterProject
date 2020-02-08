using System;
using System.Runtime.Serialization;

namespace FlightCenterProject
{
    [Serializable]
    internal class DoesNotBelongToThisAirlineCompanyException : Exception
    {
        public DoesNotBelongToThisAirlineCompanyException()
        {
        }

        public DoesNotBelongToThisAirlineCompanyException(string message) : base(message)
        {
        }

        public DoesNotBelongToThisAirlineCompanyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DoesNotBelongToThisAirlineCompanyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}