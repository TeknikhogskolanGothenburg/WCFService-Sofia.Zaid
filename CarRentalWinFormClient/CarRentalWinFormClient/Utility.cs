using CarRentalWinFormClient.CarRentalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalWinFormClient
{
    public class Utility
    {
        public static BookingService CreateNewBookingClient()
        {
            return new BookingServiceClient("NetTcpBinding_BookingService");
        }
        public static CustomerService CreateNewCustomerServiceClient()
        {
            return new CustomerServiceClient("NetTcpBinding_CustomerService");
        }

        public static CarService CreateNewCarServiceClient()
        {
            return new CarServiceClient("NetTcpBinding_CarService");
        }
    }
}
