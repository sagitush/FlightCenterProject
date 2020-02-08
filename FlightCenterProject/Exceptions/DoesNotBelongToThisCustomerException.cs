using System;
using System.Runtime.Serialization;

namespace FlightCenterProject
{
    [Serializable]
    internal class DoesNotBelongToThisCustomerException : Exception
    {
        public DoesNotBelongToThisCustomerException()
        {
        }

        public DoesNotBelongToThisCustomerException(string message) : base(message)
        {
        }

        public DoesNotBelongToThisCustomerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DoesNotBelongToThisCustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}