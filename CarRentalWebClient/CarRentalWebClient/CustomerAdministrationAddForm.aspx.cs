using CarRentalWebClient.CarRentalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;

namespace CarRentalWebClient
{
    public partial class CustomerAdministrationAddForm : System.Web.UI.Page
    {
        CustomerService client;
        private void CustomerServiceClient_Faulted(object sender, EventArgs e)
        {
            client = Utility.CreateNewCustomerServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CustomerServiceClient_Faulted);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            client = Utility.CreateNewCustomerServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CustomerServiceClient_Faulted);
        }


        protected void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = CreateNewCustomer();
                var thecustomerid = client.AddNewCustomer(customer);
                lblMessage.InnerText = "Customer saved";
                txtEmailAddress.Value = String.Empty;
                txtFirstName.Value = String.Empty;
                txtLastName.Value = String.Empty;
                txtPhoneNumber.Value = String.Empty;
            }
            catch (FaultException<RequiredInputOmitted> exc)
            {
                lblMessage.InnerText = exc.Detail.Message + " " + exc.Detail.Description;
            }
            catch (FaultException<UpdateFail> ex)
            {
                lblMessage.InnerText = "Something went wrong " + ex.Detail.Message + ", " + ex.Detail.Description;
            }
            catch (FaultException ex)
            {
                lblMessage.InnerText = ex.Message;
            }
        }

        /// <summary>
        /// Create customer object from the values 
        /// put in textbox fields in web form.
        /// </summary>
        /// <returns>A customer object.</returns>
        private Customer CreateNewCustomer()
        {
            Customer customer = new Customer
            {
                FirstName = txtFirstName.Value.ToString(),
                LastName = txtLastName.Value.ToString(),
                Email = txtEmailAddress.Value.ToString(),
                PhoneNumber = txtPhoneNumber.Value.ToString()
            };
            return customer;
        }
    }
}