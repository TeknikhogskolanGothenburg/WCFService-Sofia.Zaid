using CarRentalWebClient.CarRentalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CarRentalWebClient
{
    public partial class PickUpAndDropOffCar : System.Web.UI.Page
    {
        private CarLeasingService client;
        private CarService carClient;
        private BookingService bookingServiceClient;

        private void BookingClient_Faulted(object sender, EventArgs e)
        {
            bookingServiceClient = Utility.CreateNewBookingClient();
            ((ICommunicationObject)bookingServiceClient).Faulted += new EventHandler(BookingClient_Faulted);
        }
        private void CarLeasingClient_Faulted(object sender, EventArgs e)
        {
            client = Utility.CreateNewCarLeasingServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CarLeasingClient_Faulted);
        }
        private void CarClient_Faulted(object sender, EventArgs e)
        {
            carClient = Utility.CreateNewCarServiceClient();
            ((ICommunicationObject)carClient).Faulted += new EventHandler(CarClient_Faulted);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bookingServiceClient = Utility.CreateNewBookingClient();
            ((ICommunicationObject)bookingServiceClient).Faulted += new EventHandler(BookingClient_Faulted);

            client = Utility.CreateNewCarLeasingServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CarLeasingClient_Faulted);

            carClient = Utility.CreateNewCarServiceClient();
            ((ICommunicationObject)carClient).Faulted += new EventHandler(CarClient_Faulted);
        }

        private Booking GetBookingIfValidInput(TextBox id)
        {
            if (Utility.CheckIfTextFieldIsEmpty(id, "No input given, try again", lblMessage))
            {
                try
                {
                    Booking booking = bookingServiceClient.GetBooking(Convert.ToInt32(id.Text));
                    return booking;
                }
                catch (FaultException<EntityNotFound> ex)
                {
                    lblMessage.Text = ex.Detail.Message + " " + ex.Detail.Description;
                    return null;
                }
                catch (FaultException ex)
                {
                    lblMessage.Text = ex.Message;
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        protected void btnDropOff_Click(object sender, EventArgs e)
        {
            if (Utility.CheckIfTextFieldIsEmpty(txtBookingId))
            {

                try
                {
                    client.DropOffCar(Convert.ToInt32(txtBookingId.Text));
                    lblMessage.Text = "Car  sucessfully returned to garage.";
                    txtCarBrand.Text = String.Empty;
                    txtCarId.Text = String.Empty;
                    txtCarModel.Text = String.Empty;
                    txtBookingId.Text = String.Empty;
                    txtEndTime.Text = String.Empty;
                    txtStartTime.Text = String.Empty;
                }
                catch (FaultException<InvalidOperation> ex)
                {
                    lblMessage.Text = ex.Detail.Message + " " + ex.Detail.Description;
                }
                catch (FaultException ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
            else
            {
                lblMessage.Text = "Sorry couldn't drop off car, please try again.";
            }
        }


        protected void btnGetBooking_Click(object sender, EventArgs e)
        {
            try
            {
                Booking b = GetBookingIfValidInput(txtBookingId);
                if (null != b)
                {
                    txtCarId.Text = b.CarToBook.CarID.ToString();
                    txtEndTime.Text = b.EndDate.ToString();
                    txtStartTime.Text = b.StartDate.ToString();
                    txtCarBrand.Text = b.CarToBook.Brand;
                    txtCarModel.Text = b.CarToBook.Model;
                }
            }
            catch (FaultException<EntityNotFound> ex)
            {
                lblMessage.Text = ex.Detail.Message + " " + ex.Detail.Description;
            }
            catch (FaultException ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnPickUp_Click1(object sender, EventArgs e)
        {
            if (Utility.CheckIfTextFieldIsEmpty(txtBookingId, "Error, check input!", lblMessage))
            {
                try
                {
                    client.PickUpReservedCar(Convert.ToInt32(txtBookingId.Text));
                    lblMessage.Text = "Car successfully picked up from garage.";
                    txtCarBrand.Text = String.Empty;
                    txtCarId.Text = String.Empty;
                    txtCarModel.Text = String.Empty;
                    txtBookingId.Text = String.Empty;
                    txtEndTime.Text = String.Empty;
                    txtStartTime.Text = String.Empty;
                }
                catch (FaultException<InvalidOperation> ex)
                {
                    lblMessage.Text = ex.Detail.Message + " " + ex.Detail.Description;
                }
                catch (FaultException exc)
                {
                    lblMessage.Text = exc.Message;
                }
            }
            else
            {
                lblMessage.Text = "Sorry couldn't pick up car, review input.";
            }
        }
    }
}