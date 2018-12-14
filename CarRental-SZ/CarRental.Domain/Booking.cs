using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace CarRental.Domain
{
    [DataContract (Namespace ="http://sofiazaid.net/carRental/2018/11")]
    public class Booking
    {
        [DataMember (Name = "BookingID")]
        public int Id { get; set; }
        [DataMember (IsRequired = true, Name = "CustomerToBook")]
        public Customer BookingCustomer { get; set; }
        [DataMember (Name = "CustomerID")]
        public int CustomerId { get; set; }
        [DataMember (IsRequired = true, Name = "StartDate")]
        public DateTime StartTime { get; set; }
        [DataMember (IsRequired = true, Name = "EndDate")]
        public DateTime EndTime { get; set; }
        [DataMember(IsRequired = true, Name = "CarToBook")]
        public Car Car{ get; set; }
        [DataMember(Name = "CarID"), ForeignKey(nameof(CarId))]
        public int CarId { get; set; }
        [DataMember]
        public bool CarInGarage { get; set; } = true;
    }
}
