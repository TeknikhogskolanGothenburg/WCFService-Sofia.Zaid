using CarRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService
{
    [ServiceContract(Namespace = "http://sofiazaid.net/carRental/2018/11", Name = "CarLeasingService")]
    public interface ICarLeasingService
    {
        [OperationContract]
        [FaultContract(typeof(InvalidOperationFault))]
        Car PickUpReservedCar(int bookingId);
        [OperationContract]
        [FaultContract(typeof(InvalidOperationFault))]
        Car DropOffCar(int bookingId);
    }
}
