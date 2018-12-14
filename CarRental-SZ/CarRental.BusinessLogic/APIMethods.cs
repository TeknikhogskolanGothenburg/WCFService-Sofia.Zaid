using CarRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Data;
using Microsoft.EntityFrameworkCore;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;
using CarRentalService;

namespace CarRental.BusinessLogic
{
    public class APIMethods
    {
        private Repository repos = new Repository();

        /// <summary>
        /// Method that gets customer object with given id.
        /// </summary>
        /// <param name="customerId">Id of customer to get data on.</param>
        /// <returns>Customer object representing customer data in db for given customerid.</returns>
        public Customer GetCustomer(int customerId)
        {
            int i;
            Customer c = repos.FindBy<Customer>(customer => customer.Id == customerId).FirstOrDefault();
            if (null == c || (int.TryParse(c.Id.ToString(), out i) == false))
            {
                EntityNotFoundFault fault = new EntityNotFoundFault();
                fault.Message = "Error trying to retrieve customer data.";
                fault.Description = "No Customer with given Id in database.";
                throw new FaultException<EntityNotFoundFault>(fault);
            }
            else
            {
                return c;
            }
        }

        /// <summary>
        /// Get list of all customer objects representing all customers found in db.
        /// </summary>
        /// <returns>List of all customers.</returns>
        public List<Customer> GetCustomers()
        {
            return repos.DataSet<Customer>().ToList();
        }

        /// <summary>
        /// Add customer to db.
        /// </summary>
        /// <param name="customer">Customer object containing data to be added to db.</param>
        /// <returns>An int representing Id of added customer.</returns>
        public int AddCustomer(Customer customer)
        {
            if (String.IsNullOrEmpty(customer.FirstName) || String.IsNullOrEmpty(customer.LastName) || String.IsNullOrEmpty(customer.Email))
            {
                RequiredInputOmittedFault fault = new RequiredInputOmittedFault();
                fault.Message = "Error when trying to add customer!";
                fault.Description = "One or more of the fields: firstname, lastname, email were left empty.Can't save customer, please check your input.";
                throw new FaultException<RequiredInputOmittedFault>(fault);
            }
            else
            {
                try
                {
                    repos.Add(customer);
                    repos.SaveChanges();
                    return customer.Id;
                }
                catch (DbUpdateException ex)
                {
                    DbUpdateFault fault = new DbUpdateFault();
                    fault.Message = ex.Message;
                    fault.Description = "Could not save customer to database since the email address was already registered in system, please check that input was correct.";
                    throw new FaultException<DbUpdateFault>(fault);
                }
            }
        }

        /// <summary>
        /// Delete customer from database.
        /// </summary>
        /// <param name="customerId">Id of customer to delete.</param>
        public void DeleteCustomer(int customerId)
        {
            int i = 0;

            if (int.TryParse(customerId.ToString(), out i) == false)
            {
                InvalidIdInputFault fault = new InvalidIdInputFault();
                fault.Message = "Error with input in id field.";
                fault.Description = "Something went wrong, please check that you found an existing customer before trying to delete!";
                throw new FaultException<InvalidIdInputFault>(fault);
            }
            else
            {
                Customer c = repos.FindBy<Customer>(cust => cust.Id == customerId).FirstOrDefault();

                if (null != c)
                {
                    repos.Remove(c);
                    repos.SaveChanges();
                }
                else
                {
                    throw new FaultException("Something went wrong, please check that you found an existing customer before trying to delete!");
                }
            }
        }

        /// <summary>
        /// Edit customer data in db.
        /// </summary>
        /// <param name="customer">Customer object containing data representing the specific customer 
        /// who's data is to be edited in db.</param>
        public void EditCustomer(Customer customer)
        {
            if (String.IsNullOrEmpty(customer.FirstName) || String.IsNullOrEmpty(customer.LastName) || String.IsNullOrEmpty(customer.Email))
            {
                RequiredInputOmittedFault fault = new RequiredInputOmittedFault();
                fault.Message = "Input related error.";
                fault.Description = "One or more of the fields: firstname, lastname, email were left empty. Can't save customer, please check your input.";
                throw new FaultException<RequiredInputOmittedFault>(fault);
            }
            else
            {
                Customer c = GetCustomer(customer.Id);
                c.FirstName = customer.FirstName;
                c.LastName = customer.LastName;
                c.Email = customer.Email;
                c.PhoneNumber = customer.PhoneNumber;
                repos.SaveChanges();
            }
        }

        /// <summary>
        /// Add a car to database, check that certain property values are not null or empty- 
        /// else throw exception.
        /// </summary>
        /// <param name="car">Car object to add to system.</param>
        /// <returns>An id representing the added car.</returns>
        public int AddCar(Car car)
        {
            int i = 0;
            if (String.IsNullOrEmpty(car.Brand) || String.IsNullOrEmpty(car.Model) || String.IsNullOrEmpty(car.RegistrationNo)
                || String.IsNullOrEmpty(car.ManufacturingYear.ToString()) || (int.TryParse(car.ManufacturingYear.ToString(), out i) == false))
            {
                RequiredInputOmittedFault fault = new RequiredInputOmittedFault();
                fault.Message = "Input-related error.";
                fault.Description = "One or more of the fields: brand, model, registration number or manufacturing year were left empty. " +
                    "Can't save new car, please check your input.";
                throw new FaultException<RequiredInputOmittedFault>(fault);
            }
            else
            {
                repos.Add(car);
                repos.SaveChanges();
                return car.Id;
            }
        }

        /// <summary>
        /// Delete car from database, set property "Deleted" to true and property 
        /// "Available" to false. If already listed as deleted: throw exception.
        /// </summary>
        /// <param name="carId">Id of car to delete.</param>
        public void DeleteCar(int carId)
        {
            Car c = repos.FindBy<Car>(car => car.Id == carId).FirstOrDefault();
            List<Booking> b = GetBookings()
                .Where(book => book.CarId == carId).ToList();
            foreach (Booking book in b)
            {
                if(book.EndTime >= DateTime.Today)
                {
                    DbUpdateFault fault = new DbUpdateFault();
                    fault.Message = "Error when trying to delete car.";
                    fault.Description = "Car has active bookings, can't be deleted from system at the moment.";
                    throw new FaultException<DbUpdateFault>(fault);
                }
            }          
            if (c.Deleted)
            {
                throw new FaultException("Car already deleted in system!");
            }
            else
            {
                c.Deleted = true;
                c.AvailableForBooking = false;
                repos.Edit(c);
                repos.SaveChanges();
            }
        }

        /// <summary>
        /// Get car from database based on given id.
        /// </summary>
        /// <param name="carId">Id of car to get data on.</param>
        /// <returns>If found in db- the car object, else- throw exception.</returns>
        public Car GetCar(int carId)
        {
            int i = 0;
            Car car = repos.FindBy<Car>(c => c.Id == carId).FirstOrDefault();
            if (null == car || int.TryParse(car.Id.ToString(), out i) == false)
            {
                EntityNotFoundFault fault = new EntityNotFoundFault();
                fault.Message = "Error trying to get data on car.";
                fault.Description = "Couldn't find car with that id.";
                throw new FaultException<EntityNotFoundFault>(fault);
            }
            else
            {
                return car;
            }
        }

        /// <summary>
        /// Method where we are working with message contract and it's components
        /// request and response. Check if licensekey is correct, if so-
        /// looks up Car object based on CarId given in request object, and cast found car object 
        /// as CarInfo object. If id not found in database: throws exception. 
        /// </summary>
        /// <param name="carRequest">Request object containing data on the car we want 
        /// to retrieve info about as well as a licenseKey needed to further process 
        /// the request.</param>
        /// <returns>Carinfo object with data on specified car.</returns>
        public CarInfo GetCarInfo(CarRequest carRequest)
        {
            CarInfo x = null;
            if (carRequest.LicenseKey != "SuperSecret123")
            {
                throw new WebFaultException<string>(
                    "Wrong license key",
                    HttpStatusCode.Forbidden);
            }
            else
            {
                try
                {
                    x = repos.FindBy<Car>(c => c.Id == carRequest.CarId).Select(c => new CarInfo(c)).
                    FirstOrDefault();
                }
                catch (ArgumentNullException e)
                {
                    throw new FaultException(e.Message + "Argument null");
                }
                if (x == null || String.IsNullOrEmpty(x.Id.ToString()))
                {
                    throw new FaultException("Car with id " + carRequest.CarId + " not found");
                }
                return x;
            }
        }

        /// <summary>
        /// List available cars for a specified timespan (the time from one date to another).
        /// Car is available if not found in Booking-table during specified timespan and if value
        /// for property "Available" in Car-table is set to "true" (i.e. car is not unavailable 
        /// because of reparation, or otherwise).
        /// </summary>
        /// <param name="dateFrom">Start of timespan for when to check which cars are available.</param>
        /// <param name="dateTo">End of timespan for when to check which cars are available.</param>
        /// <returns>List of available and none-reserved cars during specified timespan.</returns>
        public List<Car> ListAvailableCars(DateTime dateFrom, DateTime dateTo)
        {
            List<Car> unavailableCars = repos.Context.Bookings
                .Where(b => (b.StartTime <= dateTo && b.StartTime >= DateTime.Today && b.EndTime >= dateFrom))
                .Select(b => b.Car)
                .ToList();

            return repos.FindBy<Car>(c => c.AvailableForBooking && !c.Deleted && !unavailableCars.Any(car => car.Id == c.Id)).ToList();
        }

        /// <summary>
        /// Register when reservation becomes an actual leasing (i.e customer gets reserved car from office).
        /// </summary>
        /// <param name="bookingId">Id of the booking for which the booked car is picked up from office.</param>
        /// <returns>The Car object to pick up for the booking in question.</returns>
        public Car PickUpCar(int bookingId)
        {
            var bookings = repos.DataSet<Booking>().ToList();
            Booking booking = repos.DataSet<Booking>()
                .Where(b => b.Id == bookingId
                         && b.StartTime <= DateTime.Today.Date
                         && b.EndTime >= DateTime.Today.Date
                         && b.CarInGarage)
                .Include(b => b.Car)
                .SingleOrDefault();
         
            if (null == booking || booking.CarInGarage == false)
            {
                InvalidOperationFault fault = new InvalidOperationFault();
                fault.Message = "Error, could not register pick up in system.";
                fault.Description = "Either there is no active booking for the given booking id " +
                    "OR customer has already picked the car up for this booking.";
                throw new FaultException<InvalidOperationFault>(fault);
            }
            Car bookedCar = booking.Car;
            booking.CarInGarage = false;
            repos.Edit(booking);
            repos.SaveChanges();
            return bookedCar;
        }

        /// <summary>
        /// To register that a customer has returned a car, looks up booking for car,
        /// returns Car object.
        /// </summary>
        /// <param name="carId">BookingId, id of booking for which a specific car is returned.</param>
        /// <returns>Car object.</returns>
        public Car DropOffCar(int bookingId)
        {
            var bookings = repos.DataSet<Booking>().ToList();
            Booking booking = repos.DataSet<Booking>()
                .Where(book => book.Id == bookingId
                && book.StartTime <= DateTime.Today.Date
                && (book.CarInGarage == false))
                .Include(book => book.Car)
                .SingleOrDefault();
           
            if(null == booking)
            {
                InvalidOperationFault fault = new InvalidOperationFault();
                fault.Message = "Error trying to return car in system.";
                fault.Description = "There is no active booking for the given booking id. If there is a car to return, please review input booking id " +
                    " to find the car you wish to return.";
                throw new FaultException<InvalidOperationFault>(fault);
            }
            Car c = booking.Car;
            booking.CarInGarage = true;
            repos.Edit(booking);
            repos.SaveChanges();
            return c;
        }

        /// <summary>
        /// Get all car objects representing all cars found in db.
        /// </summary>
        /// <returns>A list of car objects.</returns>
        public List<Car> GetCars()
        {
            return repos.DataSet<Car>().ToList();
        }

        /// <summary>
        /// Find car object by registration number.
        /// </summary>
        /// <param name="regNr">Registration number of car to get data on.</param>
        /// <returns>Car object if found, else null.</returns>
        public Car GetCarByRegNr(string regNr)
        {
            return repos.FindBy<Car>(c => c.RegistrationNo == regNr).FirstOrDefault();
        }

        /// <summary>
        /// Method to add booking to db.
        /// </summary>
        /// <param name="booking">Booking object to add.</param>
        /// <returns>If new booking created- a booking id, else- throws an exception.</returns>
        public int AddBooking(Booking booking)
        {
            if (booking.StartTime < DateTime.Today.Date)
            {
                InvalidDateException fault = new InvalidDateException();
                fault.Message = "Error with chosen date.";
                fault.Description = "Date from has to be today or later, please try again.";
                throw new FaultException<InvalidDateException>(fault);
            }
            else if (booking.EndTime < booking.StartTime || booking.EndTime < DateTime.Today.Date)
            {
                InvalidDateException fault = new InvalidDateException();
                fault.Message = "Error with chosen date.";
                fault.Description = "Date to has to be either the same as today or later, please try again.";
                throw new FaultException<InvalidDateException>(fault);
            }
            else
            {
                List<Car> availableCars = ListAvailableCars(booking.StartTime, booking.EndTime);
                bool available = availableCars.Any(c => c.Id == booking.CarId);

                if (!available)
                {
                    throw new InvalidOperationException("Something went wrong, couldn't book car.");
                }
                repos.Add(booking);
                repos.SaveChanges();
                return booking.Id;
            }
        }

        /// <summary>
        /// Delete booking from db.
        /// </summary>
        /// <param name="bookingId">Id of booking to delete.</param>
        public void DeleteBooking(int bookingId)
        {
            Booking b = repos.FindBy<Booking>(book => book.Id == bookingId).FirstOrDefault();
            if (null != b && b.CarInGarage)
            {
                try
                {
                    repos.Remove(b);
                    repos.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    DbUpdateFault faulty = new DbUpdateFault();
                    faulty.Message = ex.Message;
                    faulty.Description = "Something went wrong when trying to delete booking from db.";
                    throw new FaultException<DbUpdateFault>(faulty);
                }
            }
            else
            {
                InvalidOperationFault fault = new InvalidOperationFault();
                fault.Message = "Error when trying to delete booking.";
                fault.Description = "Booking with that id could not be deleted from database, please review input. Maybe" +
                    "the booking was not found in system or the rented car has not been returned by customer.";
                throw new FaultException<InvalidOperationFault>(fault);
            }
        }

        /// <summary>
        /// Find customer bookings by customer email address.
        /// </summary>
        /// <param name="emailAdress">String representing a customer email address.</param>
        /// <returns>If found in db- return a list of bookings, else-return empty.</returns>
        public List<Booking> FindBookingsByCustomerEmail(string emailAdress)
        {
            return repos.DataSet<Booking>()
               .Include(c => c.Car)
              .Where(c => c.BookingCustomer.Email == emailAdress).ToList();
        }

        /// <summary>
        /// Get data on booking object with given bookingid.
        /// </summary>
        /// <param name="bookingId">Id of the booking to get data on.</param>
        /// <returns>Booking object with given id.</returns>
        public Booking GetBooking(int bookingId)
        {
            int i = 0;

            if (null == repos.FindBy<Booking>(booking => booking.Id == bookingId).FirstOrDefault()
                || int.TryParse(bookingId.ToString(), out i) == false)
            {
                EntityNotFoundFault fault = new EntityNotFoundFault
                {
                    Message = "Error-no booking found.",
                    Description = "Could not find booking with that id in database, please try again."
                };
                throw new FaultException<EntityNotFoundFault>(fault);
            }
            else
            {
                return repos.DataSet<Booking>()
               .Where(b => b.Id == bookingId)
               .Include(b => b.BookingCustomer)
               .Include(c => c.Car)
               .FirstOrDefault();
            }
        }

        /// <summary>
        /// Get all booking objects representing all bookings in db.
        /// </summary>
        /// <returns>A list of booking objects.</returns>
        public List<Booking> GetBookings()
        {
            return repos.DataSet<Booking>().ToList();
        }
    }
}