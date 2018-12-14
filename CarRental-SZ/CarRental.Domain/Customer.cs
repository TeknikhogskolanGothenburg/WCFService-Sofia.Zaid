using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace CarRental.Domain
{
    [DataContract (Namespace ="http://sofiazaid.net/carRental/2018/11")]
    public class Customer
    {
        [DataMember (Name = "CustomerID")]
        public int Id { get; set; }

        [DataMember(Name = "FirstName", IsRequired = true)]
        public string FirstName { get; set; }

        [DataMember(Name = "LastName", IsRequired = true)]
        public string LastName { get; set; }

        [DataMember(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "Email", IsRequired = true)]
        public string Email { get; set; }

        public List<Booking> Bookings {get; set;}
    }
}
