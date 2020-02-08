using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class Ticket: IPoco
    {
        public long Id { get; set; }
        public long FlightId { get; set; }
        public long CustomerId { get; set; }

        public Ticket()
        {
                
        }

        public Ticket(long flightId, long customerId)
        {
            FlightId = flightId;
            CustomerId = customerId;
        }

        public static bool operator ==(Ticket ticket1, Ticket ticket2)
        {
            if (ReferenceEquals(ticket1, null) && ReferenceEquals(ticket2, null))
                return true;
            if (ReferenceEquals(ticket1, null) || ReferenceEquals(ticket2, null))
                return false;

            return (ticket1.Id == ticket2.Id);
        }
        public static bool operator !=(Ticket ticket1, Ticket ticket2)
        {
            return !(ticket1 == ticket2);
        }

        public override bool Equals(object ob)
        {
            if (ReferenceEquals(ob, null))
                return false;
            Ticket c = ob as Ticket;
            if (ReferenceEquals(c, null))
                return false;

            return this.Id == c.Id;
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(this.Id);
        }

    }
}
