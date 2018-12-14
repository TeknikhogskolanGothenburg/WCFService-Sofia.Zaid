using System.Runtime.Serialization;

namespace CarRentalService
{
    [DataContract(Namespace = "http://sofiazaid.net/carRental/2018/11", Name = "EntityNotFound")]
    public class EntityNotFoundFault
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}