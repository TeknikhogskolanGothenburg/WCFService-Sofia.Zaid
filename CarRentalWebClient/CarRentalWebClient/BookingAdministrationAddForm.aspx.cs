using CarRentalWebClient.CarRentalService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalWebClient
{
    public partial class BookingAdministrationAddForm : System.Web.UI.Page
    {
        private BookingService bookingServiceClient;
        private CustomerService customerServiceClient;

        private void BookingClient_Faulted(object sender, EventArgs e)
        {
            bookingServiceClient = Utility.CreateNewBookingClient();
            ((ICommunicationObject)bookingServiceClient).Faulted += new EventHandler(BookingClient_Faulted);
        }

        private void CustomerServiceClient_Faulted(object sender, EventArgs e)
        {
            customerServiceClient = Utility.CreateNewCustomerServiceClient();
            ((ICommunicationObject)customerServiceClient).Faulted += new EventHandler(CustomerServiceClient_Faulted);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lstBoxConfirmation.Items.Clear();
            bookingServiceClient = Utility.CreateNewBookingClient();
            ((ICommunicationObject)bookingServiceClient).Faulted += new EventHandler(BookingClient_Faulted);
            customerServiceClient = Utility.CreateNewCustomerServiceClient();
            ((ICommunicationObject)customerServiceClient).Faulted += new EventHandler(CustomerServiceClient_Faulted);
        }

        protected void CalendarFrom_SelectionChanged(object sender, EventArgs e)
        {
            if (CalendarFrom.SelectedDate < DateTime.Today)
            {
                Response.Write("<script>alert('Chosen date is before today, please choose a date that is today or in the future!');</script>");
            }
        }

        protected void CalendarTo_SelectionChanged(object sender, EventArgs e)
        {
            if (CalendarTo.SelectedDate < CalendarFrom.SelectedDate || CalendarTo.SelectedDate < DateTime.Today)
            {
                Response.Write("<script>alert('Chosen to-date is before from-date, please check to-date.');</script>");
            }
        }

        /// <summary>
        /// Method that gets all car objects that are not already booked 
        /// for the user given time span.
        /// </summary>
        private void GetFreeCars()
        {
            try
            {
                List<Car> cars = new List<Car>(bookingServiceClient.GetListOfAvailableCars(CalendarFrom.SelectedDate, CalendarTo.SelectedDate));

                lstBoxAvailableCars.Items.Clear();
                foreach (Car c in cars)
                {
                    lstBoxAvailableCars.Items.Add(new ListItem($"{c.Brand}, {c.Model}, {c.RegNumber}", c.CarID.ToString()));
                }
            }
            catch (EndpointNotFoundException)
            {
                Response.Write("<script>alert('Endpoint not found, please check connection.');</script>");
            }
            catch (TimeoutException ex)
            {
                lblErrorNocarChosen.Text = ex.Message;
            }
            catch (CommunicationObjectFaultedException)
            {
                Response.Write("<script>alert('Communicaton channel not working, can't communicate with service');</script>");
            }
        }

        /// <summary>
        /// Method that checks if a given from-date is today or after today 
        /// and does not equal null.
        /// </summary>
        /// <returns>True if today or after and not equal to null,
        /// else false.</returns>
        private bool CheckFromDate()
        {
            if (CalendarFrom.SelectedDate < DateTime.Today || null == CalendarFrom.SelectedDate)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method that checks if a given to-date is after given from-date and 
        /// is not null.
        /// </summary>
        /// <returns>True if date is after from-date and not equal to null, 
        /// else false.</returns>
        private bool CheckToDate()
        {
            if (CalendarTo.SelectedDate < CalendarFrom.SelectedDate || null == CalendarTo.SelectedDate)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Eventhandler for button "Find available cars".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void searchForCars_Click(object sender, EventArgs e)
        {

            if (CheckFromDate())
            {
                if (CheckToDate())
                {
                    GetFreeCars();
                }
                else
                {
                    Response.Write("<script>alert('Error with date/dates, please check that you have chosen valid from and to date for booking.');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Error with date/dates, please check that you have chosen valid from and to date for booking.');</script>");
            }
        }

        /// <summary>
        /// Eventhandler for button "Make booking for selected car".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChooseCar_Click(object sender, EventArgs e)
        {
            try
            {
                int bookingId = bookingServiceClient.AddBooking(CreateBooking());
                Booking b = bookingServiceClient.GetBooking(bookingId);
                lstBoxConfirmation.Items.Add(new ListItem($"{b.CustomerToBook.FirstName}, {b.CustomerToBook.LastName}, " +
                    $"{b.CarToBook.Brand}, {b.CarToBook.Model}, {b.StartDate}, {b.EndDate}"));
                txtCustomerID.Text = String.Empty;
                txtEmail.Text = String.Empty;
                txtFirstName.Text = String.Empty;
                txtLastName.Text = String.Empty;
                txtPhone.Text = String.Empty;
            }
            catch (FaultException ex)
            {
                lblErrorNocarChosen.Text = ex.Message;
            }
            catch (TimeoutException ex)
            {
                lblErrorNocarChosen.Text = ex.Message;
            }
            catch (EndpointNotFoundException)
            {
                Response.Write("<script>alert('Endpoint not found, please check connection.');</script>");
            }
        }

        /// <summary>
        /// Creates a booking object from data put into text fields in form.
        /// </summary>
        /// <returns>Booking object.</returns>
        private Booking CreateBooking()
        {
            Booking booking = new Booking();
            try
            {
                booking.CustomerID = Convert.ToInt32(txtCustomerID.Text);
                booking.StartDate = CalendarFrom.SelectedDate;
                booking.EndDate = CalendarTo.SelectedDate;
            }
            catch (FaultException<InvalidDateFault> ex)
            {
                lblErrorNocarChosen.Text = ex.Detail.Message + " " + ex.Detail.Description;
            }
            if (lstBoxAvailableCars.SelectedIndex == -1)
            {
                Response.Write("<script>alert('Please select a car to book before proceeding with booking!');</script>");
            }
            else
            {
                booking.CarID = Convert.ToInt32(lstBoxAvailableCars.SelectedValue);
            }
            return booking;
        }

        /// <summary>
        /// Eventhandler for button "find customer".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFindCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                GetCustomerIfValidId(txtCustomerID);
                btnChooseCar.Enabled = true;
            }
            catch (FaultException<EntityNotFound> ex)
            {
                lblErrorCustomer.Text = ex.Detail.Message + " " + ex.Detail.Description;
            }
            catch (FaultException exc)
            {
                lblErrorCustomer.Text = exc.Message;
            }
            catch (EndpointNotFoundException)
            {
                Response.Write("<script>alert('Endpoint not found, please check connection.');</script>");
            }
        }

        private Customer GetCustomerIfValidId(TextBox id)
        {
            if (Utility.CheckIfTextFieldIsEmpty(id, "Error, check input in customer-id field!", lblErrorCustomer))
            {
                try
                {
                    Customer customer = customerServiceClient.GetCustomer(Convert.ToInt32(id.Text));
                    txtFirstName.Text = customer.FirstName;
                    txtLastName.Text = customer.LastName;
                    txtEmail.Text = customer.Email;
                    txtPhone.Text = customer.PhoneNumber;
                    return customer;
                }
                catch (FaultException<EntityNotFound> ex)
                {
                    lblErrorCustomer.Text = ex.Detail.Description;
                    return null;
                }
                catch (FaultException exc)
                {
                    lblErrorCustomer.Text = exc.Message;
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