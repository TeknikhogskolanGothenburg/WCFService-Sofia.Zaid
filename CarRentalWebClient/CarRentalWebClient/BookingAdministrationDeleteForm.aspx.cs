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
    public partial class BookingAdministrationEditOrDeleteForm : System.Web.UI.Page
    {
        private BookingService bookingServiceClient;

        /// <summary>
        /// Method that creates a new booking client instance if the first instance has got in to communication
        /// object faulted state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookingClient_Faulted(object sender, EventArgs e)
        {
            bookingServiceClient = Utility.CreateNewBookingClient();
            ((ICommunicationObject)bookingServiceClient).Faulted += new EventHandler(BookingClient_Faulted);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bookingServiceClient = Utility.CreateNewBookingClient();
            ((ICommunicationObject)bookingServiceClient).Faulted += new EventHandler(BookingClient_Faulted);
        }

        protected void btnGetBookingById_Click(object sender, EventArgs e)
        {
            try
            {
                Booking b = GetBookingIfValidId(txtbookingId);
                if (null != b)
                {
                    txtCarBrand.Value = b.CarToBook.Brand;
                    txtCarModel.Value = b.CarToBook.Model;
                    txtEmailAddress.Value = b.CustomerToBook.Email;
                    txtEndTime.Value = b.EndDate.ToShortDateString();
                    txtStartTime.Value = b.StartDate.ToShortDateString();
                    txtManYear.Value = b.CarToBook.ManuFacturingYear.ToString();
                }
            }
            catch (FaultException<EntityNotFound> ex)
            {
                lblMessage.Text = ex.Detail.Message + " " + ex.Detail.Description;
            }
            catch (FaultException exc)
            {
                lblMessage.Text = exc.Message;
            }
        }

        /// <summary>
        /// Method that checks if a textfield has any chars in it.
        /// </summary>
        /// <param name="text">textfield to check for chars.</param>
        /// <returns>True if length of text-field text is bigger than 0, else false.</returns>
        private bool CheckIfSearchParameterProvided(HtmlInputText text)
        {
            if (text.Value.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnDeleteBooking_Click(object sender, EventArgs e)
        {

            Booking b  = GetBookingIfValidId(txtbookingId);
            if (null != b)
            {
                try
                {
                    bookingServiceClient.DeleteBooking(Convert.ToInt32(txtbookingId.Value));
                    txtCarBrand.Value = String.Empty;
                    txtCarModel.Value = String.Empty;
                    txtEmailAddress.Value = String.Empty;
                    txtEndTime.Value = String.Empty;
                    txtStartTime.Value = String.Empty;
                    txtManYear.Value = String.Empty;
                    Response.Write("<script>alert('booking permanently deleted from system');</script>");
                }
                catch (FaultException<UpdateFail> exc)
                {
                    lblMessage.Text = exc.Detail.Message + " " + exc.Detail.Description;
                }
                catch (FaultException<InvalidOperation> exc)
                {
                    lblMessage.Text = exc.Detail.Message + " " + exc.Detail.Description;
                }
                catch (FaultException ex)
                {
                    lblMessage.Text = ex.Message;
                }
                catch (EndpointNotFoundException)
                {
                    Response.Write("<script>alert('Endpoint not found, please check connection.');</script>");
                }
            }
            else
            {
                Response.Redirect("ErrorPageBookingNotFound.aspx");
            }
        }

        protected void btnGetBookingByCustomerEmail_Click(object sender, EventArgs e)
        {
            if (CheckIfSearchParameterProvided(txtEmailAddress))
            {
                try
                {
                    Booking b = bookingServiceClient.FindBookingsByCustomerEmail(txtEmailAddress.Value).FirstOrDefault();
                    if (b == null)
                    {
                        Response.Redirect("ErrorPageBookingNotFound.aspx");
                    }
                    else if(!CheckIfTextFieldIsEmpty(txtbookingId))
                    {
                        lblMessage.Text = "Not a valid booking id, try again!";
                    }
                    else
                    {
                        txtCarBrand.Value = b.CarToBook.Brand;
                        txtCarModel.Value = b.CarToBook.Model;
                        txtManYear.Value = b.CarToBook.ManuFacturingYear.ToString();
                        txtStartTime.Value = b.StartDate.ToString();
                        txtEndTime.Value = b.EndDate.ToString();
                        txtbookingId.Value = b.BookingID.ToString();
                    }
                }
                catch (EndpointNotFoundException)
                {
                    Response.Write("<script>alert('Endpoint not found, please check connection.');</script>");
                }
            }
            else
            {
                Response.Redirect("ErrorPageBookingNotFound.aspx");
            }
        }

        private bool CheckIfTextFieldIsEmpty(HtmlInputText id)
        {
            int i = 0;
            var valid = int.TryParse(id.Value, out i) == true;
            if (id.Value != "" && valid == true)
            {
                return true;
            }
            else
            {
                lblMessage.Text = "review input in booking-id text field, can't find booking.";
                return false;
            }
        }

        /// <summary>
        /// Finds booking object based on input id.
        /// </summary>
        /// <param name="id">Id of the booking to get data on.</param>
        /// <returns>Booking if found, null if not found.</returns>
        private Booking GetBookingIfValidId(HtmlInputText id)
        {
            if (CheckIfTextFieldIsEmpty(id))
            {
                try
                {
                    Booking booking = bookingServiceClient.GetBooking(Convert.ToInt32(txtbookingId.Value));
                    return booking;
                }
                catch (FaultException<EntityNotFound> ex)
                {
                    lblMessage.Text = ex.Detail.Message + " " + ex.Detail.Description;
                    return null;
                }
                catch (EndpointNotFoundException)
                {
                    Response.Write("<script>alert('Endpoint not found, please check connection.');</script>");
                    return null;
                }
                catch (CommunicationObjectFaultedException)
                {
                    Response.Write("<script>alert('Something went wrong when communicating with server, please contact administrator of service.');</script>");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}