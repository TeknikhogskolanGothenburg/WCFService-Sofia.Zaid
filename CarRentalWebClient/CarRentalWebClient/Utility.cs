using CarRentalWebClient.CarRentalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CarRentalWebClient
{
    public class Utility
    {
        public static BookingService CreateNewBookingClient()
        {
            return new BookingServiceClient("WSHttpBinding_BookingService");
        }
        public static CustomerService CreateNewCustomerServiceClient()
        {
            return new CustomerServiceClient("WSHttpBinding_CustomerService");
        }

        public static CarService CreateNewCarServiceClient()
        {
            return new CarServiceClient("WSHttpBinding_CarService");
        }

        public static CarLeasingService CreateNewCarLeasingServiceClient()
        {
            return new CarLeasingServiceClient ("WSHttpBinding_CarLeasingService");
        }

        public static bool CheckIfTextFieldIsEmpty(TextBox id, string errorMessage, Label label)
        {
            int i = 0;
            var valid = int.TryParse(id.Text, out i) == true;
            if (id.Text != "" && valid == true)
            {
                return true;
            }
            else
            {
                label.Text = errorMessage;
                return false;
            }
        }

        public static bool CheckIfTextFieldIsEmpty(TextBox id)
        {
            int i = 0;
            var valid = int.TryParse(id.Text, out i) == true;
            if (id.Text != "" && valid == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}