using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace CarRental.Domain
{
    [DataContract(Namespace = "http://sofiazaid.net/carRental/2018/11")]
    public class Car
    {
        [DataMember(Name = "CarID")]
        public int Id { get; set; }

        [DataMember(Name = "RegNumber")]
        public string RegistrationNo { get; set; }
        [DataMember(Name = "Brand")]
        public string Brand { get; set; }
        [DataMember(Name = "Model")]
        public string Model { get; set; }
        [DataMember(Name = "ManuFacturing Year")]
        public int ManufacturingYear { get; set; }
        [DataMember(Name = "Available")]
        public bool AvailableForBooking { get; set; } = true;
        [DataMember(Name = "Deleted")]
        public bool Deleted { get; set; } = false;
    }
}