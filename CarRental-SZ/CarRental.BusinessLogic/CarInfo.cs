using CarRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BusinessLogic
{
    /// <summary>
    /// In this class we define how a response message from the server to the client regarding info about
    /// a car object, should be structured.
    /// </summary>
    [MessageContract(IsWrapped = true,
    WrapperName = "CarInfoObject",
    WrapperNamespace = "http://sofiazaid.net/carRental/car/2018/11")]
    public class CarInfo
    {
        public CarInfo() { }

        public CarInfo(Car car)
        {
            this.Id = car.Id;
            this.Brand = car.Brand;
            this.Model = car.Model;
            this.ManufacturingYear = car.ManufacturingYear;
            this.RegistrationNumber = car.RegistrationNo;
        }

        [MessageBodyMember(Order = 1, Namespace = "http://sofiazaid.net/carRental/car/2018/11")]
        public int Id { get; set; }

        [MessageBodyMember(Order = 2, Namespace = "http://sofiazaid.net/carRental/car/2018/11")]
        public string Model { get; set; }

        [MessageBodyMember(Order = 3, Namespace = "http://sofiazaid.net/carRental/car/2018/11")]
        public string Brand { get; set; }

        [MessageBodyMember(Order = 4, Namespace = "http://sofiazaid.net/carRental/car/2018/11")]
        public string RegistrationNumber { get; set; }

        [MessageBodyMember(Order = 5, Namespace = "http://sofiazaid.net/carRental/car/2018/11")]
        public int ManufacturingYear { get; set; }

        [MessageBodyMember(Order = 6, Namespace = "http://sofiazaid.net/carRental/car/2018/11")]
        public bool Available { get; set; } = true;

        [MessageBodyMember(Order = 7, Namespace = "http://sofiazaid.net/carRental/car/2018/11")]
        public bool Deleted { get; set; } = false;

        public Car ToCar()
        {
            Car car = new Car();
            car.Id = Id;
            car.Model = Model;
            car.RegistrationNo = RegistrationNumber;
            car.ManufacturingYear = ManufacturingYear;
            car.Deleted = Deleted;
            car.AvailableForBooking = Available;
            return car;
        }
    }
}
