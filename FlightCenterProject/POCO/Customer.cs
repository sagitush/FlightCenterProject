using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenterProject
{
   public class Customer:IPoco, IUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CreditCardNumber { get; set; }

        public Customer()
        {
                
        }

        public Customer(string firstName, string lastName, string userName, string password, string address, string phoneNo, string creditCardNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Address = address;
            PhoneNo = phoneNo;
            CreditCardNumber = creditCardNumber;
        }

        public static bool operator ==(Customer customer1, Customer customer2)
        {
            if (ReferenceEquals(customer1, null) && ReferenceEquals(customer2, null))
                return true;
            if (ReferenceEquals(customer1, null) || ReferenceEquals(customer2, null))
                return false;

            return (customer1.Id == customer2.Id);
        }
        public static bool operator !=(Customer customer1, Customer customer2)
        {
            return !(customer1 == customer2);
        }

        public override bool Equals(object ob)
        {
            if (ReferenceEquals(ob, null))
                return false;
            Customer c = ob as Customer;
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
