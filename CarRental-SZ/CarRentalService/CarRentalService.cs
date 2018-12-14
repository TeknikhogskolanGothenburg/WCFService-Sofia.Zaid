using CarRental.BusinessLogic;
using CarRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net;

namespace CarRentalService
{
    /// <summary>
    /// Implementation class for CarRental service. Implements several service interfaces (endpoints) concerning different 
    /// aspects related to a car rental business. 
    /// </summary>
    [ServiceBehavior(Namespace = "http://sofiazaid.net/carRental/2018/11")]
    public class CarRentalService : ICarService, ICustomerService, IBookingService, ICarLeasingService
    {
        private APIMethods api = new APIMethods();

        public int AddNewCustomer(Customer customer)
        {
            return api.AddCustomer(customer);
        }

        public void ChangeCustomer(Customer customer)
        {
            api.EditCustomer(customer);
        }

        public Customer GetCustomer(int id)
        {
            return api.GetCustomer(id);

        }

        public void DeleteCustomer(int id)
        {
            api.DeleteCustomer(id);
        }

        public int AddCar(Car car)
        {
            return api.AddCar(car);
        }

        public void DeleteCar(int id)
        {
            api.DeleteCar(id);
        }

        public Car GetCarByRegNr(string regNr)
        {
            return api.GetCarByRegNr(regNr);
        }

        public Car GetCarById(int id)
        {
            return api.GetCar(id);
        }

        public List<Car> GetListOfAvailableCars(DateTime from, DateTime to)
        {
            return api.ListAvailableCars(from, to);
        }

        public int AddBooking(Booking booking)
        {
            return api.AddBooking(booking);
        }

        public void DeleteBooking(int id)
        {
            api.DeleteBooking(id);
        }

        public Booking GetBooking(int id)
        {
            return api.GetBooking(id);
        }

        public Car PickUpReservedCar(int bookingId)
        {
            return api.PickUpCar(bookingId);
        }

        public Car DropOffCar(int bookingId)
        {
            return api.DropOffCar(bookingId);
        }

        public List<Booking> FindBookingsByCustomerEmail(string emailAdress)
        {
            return api.FindBookingsByCustomerEmail(emailAdress);
        }

        public CarInfo GetCarInfo(CarRequest request)
        {
            return api.GetCarInfo(request);
        }
    }
}