using CarRentalWinFormClient.CarRentalService;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Forms;

namespace CarRentalWinFormClient
{
    public partial class StartPage : Form
    {
        private CarService client;
        private BookingService bookingServiceClient;
        private CustomerService customerClient;
        private void CarClient_Faulted(object sender, EventArgs e)
        {
            client = Utility.CreateNewCarServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CarClient_Faulted);
        }
        private void BookingClient_Faulted(object sender, EventArgs e)
        {
            bookingServiceClient = Utility.CreateNewBookingClient();
            ((ICommunicationObject)bookingServiceClient).Faulted += new EventHandler(BookingClient_Faulted);
        }
        private void CustomerServiceClient_Faulted(object sender, EventArgs e)
        {
            customerClient = Utility.CreateNewCustomerServiceClient();
            ((ICommunicationObject)customerClient).Faulted += new EventHandler(CustomerServiceClient_Faulted);
        }

        public StartPage()
        {
            InitializeComponent();
            client = Utility.CreateNewCarServiceClient();
            bookingServiceClient = Utility.CreateNewBookingClient();
            customerClient = Utility.CreateNewCustomerServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CarClient_Faulted);
            ((ICommunicationObject)bookingServiceClient).Faulted += new EventHandler(BookingClient_Faulted);
            ((ICommunicationObject)customerClient).Faulted += new EventHandler(CustomerServiceClient_Faulted);
        }

        private void btnSaveCar_Click(object sender, EventArgs e)
        {
            try
            {
                Car car = CreateNewCar();
                if (client.GetCarByRegNr(car.RegNumber = txtRegNr.Text) != null)
                {
                    MessageBox.Show("Car with this registration number is already in database," +
                        "please try again!");
                }
                else
                {
                    var theCarId = client.AddCar(car);
                    MessageBox.Show("Car saved");
                    txtRegNr.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtManYear.Text = string.Empty;
                    txtBrand.Text = string.Empty;
                }
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnFindExistingCar_Click(object sender, EventArgs e)
        {
            Car car = GetCarIfValidInput(txtCarId);

            if (car == null)
            {
                MessageBox.Show("Car not found in system, please check the id");
            }
            else
            {
                txtboxBrandDelete.Text = car.Brand;
                txtModelDelete.Text = car.Model;
                txtRegNrDelete.Text = car.RegNumber;
                txtManYearDelete.Text = car.ManuFacturingYear.ToString();
            }

        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            Car car = GetCarIfValidInput(txtCarId);
            if (car == null)
            {
                MessageBox.Show("Car wasn't found in system");
            }
            else
            {
                try
                {
                    client.DeleteCar(Convert.ToInt32(txtCarId.Text));
                    MessageBox.Show("Car deleted from system!");
                }
                catch (FaultException<UpdateFail> ex)
                {
                    MessageBox.Show(ex.Detail.Message + " " + ex.Detail.Description);
                }
                txtboxBrandDelete.Text = "";
                txtModelDelete.Text = "";
                txtManYearDelete.Text = "";
                txtRegNrDelete.Text = "";
                txtCarId.Text = "";
            }
        }

        private bool CheckIfTextFieldIsEmpty(TextBox id)
        {
            if (id.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Car GetCarIfValidInput(TextBox id)
        {
            if (CheckIfTextFieldIsEmpty(id))
            {
                Car car = client.GetCarById(Convert.ToInt32(txtCarId.Text));
                return car;
            }
            else
            {
                return null;
            }
        }

        private Car CreateNewCar()
        {
            int i = 0;
            Car car = new Car
            {
                Brand = txtBrand.Text,
                Model = txtModel.Text,
                RegNumber = txtRegNr.Text

            };
            if (int.TryParse(txtManYear.Text, out i))
            {
                car.ManuFacturingYear = Convert.ToInt32(txtManYear.Text);
            }
            return car;
        }

        private void calDateFrom_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (calDateFrom.SelectionRange.Start.Date < DateTime.Today)
            {
                MessageBox.Show("Chosen date is before today, please choose a date that is today or in the future!");
            }
        }

        private void btnFindAvailableCars_Click(object sender, EventArgs e)
        {
            if (CheckFromDate())
            {
                if (CheckToDate())
                {
                    GetFreeCars();
                }
                else
                {
                    MessageBox.Show("Error with date/dates, please check that you have chosen valid from and to date for booking.");
                }
            }
            else
            {
                MessageBox.Show("Error with date/dates, please check that you have chosen valid from and to date for booking.");
            }
        }

        private void calDateTo_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (calDateTo.SelectionRange.Start.Date < calDateFrom.SelectionRange.Start.Date || calDateTo.SelectionRange.Start.Date < DateTime.Today)
            {
                MessageBox.Show("Chosen to-date is before from-date, please check to-date.");
            }
        }

        private void GetFreeCars()
        {
            try
            {
                List<Car> cars = new List<Car>(bookingServiceClient.GetListOfAvailableCars(calDateFrom.SelectionRange.Start.Date, calDateTo.SelectionRange.Start.Date));

                lstCarsAvailable.Items.Clear();
                foreach (Car c in cars)
                {
                    lstCarsAvailable.Items.Add(new ListBoxItemObject($"{c.Brand}, {c.Model}, {c.RegNumber}", c.CarID));
                }
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Endpoint not found, please check connection.");
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show("Communicaton channel not working, can't communicate with service");
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
            if (calDateFrom.SelectionRange.Start.Date < DateTime.Today || null == calDateFrom.SelectionRange.Start.Date)
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
            if (calDateTo.SelectionRange.Start.Date < calDateFrom.SelectionRange.Start.Date || null == calDateTo.SelectionRange.Start.Date)
            {
                return false;
            }
            return true;
        }

        private Booking CreateBooking()
        {
            Booking booking = new Booking();

            try
            {
                booking.CustomerID = Convert.ToInt32(txtCId.Text);
                booking.StartDate = calDateFrom.SelectionRange.Start.Date;
                booking.EndDate = calDateTo.SelectionRange.Start.Date;
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (lstCarsAvailable.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a car to book before proceeding with booking!");
            }
            else
            {
                booking.CarID = Convert.ToInt32(((ListBoxItemObject)lstCarsAvailable.SelectedItem).SelectedItemId);
            }
            return booking;
        }

        private void btnMakeBooking_Click(object sender, EventArgs e)
        {
            try
            {
                int bookingId = bookingServiceClient.AddBooking(CreateBooking());
                Booking b = bookingServiceClient.GetBooking(bookingId);
                lstBookingConfirmation.Items.Add(new ListBoxItemObject($"{b.CustomerToBook.FirstName}, {b.CustomerToBook.LastName}, " +
                    $"{b.CarToBook.Brand}, {b.CarToBook.Model}, {b.StartDate}, {b.EndDate}"));
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show(ex.Message);
            }     
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Endpoint not found, please check connection.");
            }
        }

        private void btnFindC_Click(object sender, EventArgs e)
        {
            int i = 0;

            try
            {
                if ((int.TryParse(txtCId.Text, out i) == false))
                {
                    MessageBox.Show("review input in customer-id text field.");
                }
                else
                {
                    Customer customer = customerClient.GetCustomer(Convert.ToInt32(txtCId.Text));
                    txtCFirstName.Text = customer.FirstName;
                    txtCLastName.Text = customer.LastName;
                    txtEmail.Text= customer.Email;
                    txtPhone.Text = customer.PhoneNumber;
                    btnMakeBooking.Enabled = true;
                }
            }
            catch (FaultException<EntityNotFound> ex)
            {
                MessageBox.Show(ex.Detail.Message + " " + ex.Detail.Description);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Endpoint not found, please check connection.");
            }
        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = CreateNewCustomer();
                var thecustomerid = customerClient.AddNewCustomer(customer);
                MessageBox.Show("Customer saved");
                txtEmail.Text = String.Empty;
                txtFirstName.Text = String.Empty;
                txtLastName.Text = String.Empty;
                txtPhone.Text = String.Empty;
            }
            catch (FaultException<RequiredInputOmitted> exc)
            {
                MessageBox.Show(exc.Detail.Message + " " + exc.Detail.Description);
            }
            catch (FaultException<UpdateFail> ex)
            {
                MessageBox.Show(ex.Detail.Message + ", " + ex.Detail.Description);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Customer CreateNewCustomer()
        {
            Customer customer = new Customer
            {
                FirstName = txtLastName.Text.ToString(),
                LastName = txtLastName.ToString(),
                Email = txtEmail.Text.ToString(),
                PhoneNumber = txtPhone.Text.ToString()
            };
            return customer;
        }

        private void btnFindCustomer_Click(object sender, EventArgs e)
        {
            int i = 0;

            try
            {
                if ((int.TryParse(txtCustomerID.Text, out i) == false))
                {
                    MessageBox.Show("review input in customer-id text field.");
                }
                else
                {
                    Customer customer = customerClient.GetCustomer(Convert.ToInt32(txtCustomerID.Text));
                    txtFirstName.Text = customer.FirstName;
                    txtLastName.Text = customer.LastName;
                    txtEmail.Text = customer.Email;
                }
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Endpoint not found, please check connection.");
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            customerClient.DeleteCustomer((Convert.ToInt32(txtCustomerID.Text)));
            MessageBox.Show("Customer deleted!");
        }

        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            try
            {
                Booking b = GetBookingIfValidId(txtBookingId);
                if (null != b)
                {
                    txtCarBrand.Text = b.CarToBook.Brand;
                    txtCarModel.Text = b.CarToBook.Model;
                    txtEndTime.Text = b.EndDate.ToShortDateString();
                    txtStartTime.Text = b.StartDate.ToShortDateString();
                    txtCustomerEmailAddress.Text = b.CustomerToBook.Email;
                }
            }
            catch (FaultException<EntityNotFound> ex)
            {
                MessageBox.Show(ex.Detail.Description + " " + ex.Detail.Message);
            }
            catch (FaultException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private Booking GetBookingIfValidId(TextBox id)
        {
            if (CheckIfTextFieldIsEmpty(id))
            {
                try
                {
                    Booking booking = bookingServiceClient.GetBooking(Convert.ToInt32(txtBookingId.Text));
                    return booking;
                }
                catch (FaultException<EntityNotFound> ex)
                {
                    MessageBox.Show(ex.Detail.Message + " " + ex.Detail.Description);
                    return null;
                }
                catch (EndpointNotFoundException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void btnDeleteBooking_Click(object sender, EventArgs e)
        {
            Booking b = GetBookingIfValidId(txtBookingId);
            if (null != b)
            {
                try
                {
                    bookingServiceClient.DeleteBooking(Convert.ToInt32(txtBookingId.Text));
                    txtCarBrand.Text = String.Empty;
                    txtCarModel.Text = String.Empty;
                    txtEndTime.Text = String.Empty;
                    txtStartTime.Text = String.Empty;
                    txtCustomerEmailAddress.Text = String.Empty;
                    MessageBox.Show("booking permanently deleted from system");
                }
                catch (FaultException<UpdateFail> exc)
                {
                    MessageBox.Show(exc.Detail.Message + " " + exc.Detail.Description);
                }
                catch (FaultException<InvalidOperation> exc)
                {
                    MessageBox.Show(exc.Detail.Message + " " + exc.Detail.Description);
                }
                catch (FaultException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Endpoint not found, please check connection");
                }
            }
            else
            {
                MessageBox.Show("Booking not found!");
            }
        }
    }
}
