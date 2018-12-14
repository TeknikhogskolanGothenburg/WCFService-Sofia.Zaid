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
    public partial class CustomerAdministrationEditOrDeleteForm : System.Web.UI.Page
    {
        private CustomerService client;
        private void CustomerClient_Faulted(object sender, EventArgs e)
        {
            client = Utility.CreateNewCustomerServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CustomerClient_Faulted);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            client = Utility.CreateNewCustomerServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CustomerClient_Faulted);
        }

        protected void btnGetCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = GetCustomerIfValidId(txtId);
                if (null != customer)
                {
                    txtFirstName.Text = customer.FirstName;
                    txtLastName.Text = customer.LastName;
                    txtEmailAddress.Text = customer.Email;
                    txtPhoneNumber.Text = customer.PhoneNumber;
                }
            }
            catch (FaultException<EntityNotFound> ex)
            {
                lblCustomerSaved.Text = ex.Detail.Description;
            }
        }

        protected void btnSaveEditedCustomer_Click(object sender, EventArgs e)
        {
            Customer customer = GetCustomerIfValidId(txtId);
            if (null != customer)
            {
                customer.FirstName = txtFirstName.Text;
                customer.LastName = txtLastName.Text;
                customer.PhoneNumber = txtPhoneNumber.Text;
                customer.Email = txtEmailAddress.Text;
                try
                {
                    client.ChangeCustomer(customer);
                    txtLastName.Text = String.Empty;
                    txtFirstName.Text = String.Empty;
                    txtPhoneNumber.Text = String.Empty;
                    txtEmailAddress.Text = String.Empty;
                    lblCustomerSaved.Text = "Edited customer information was saved.";
                }
                catch(FaultException<RequiredInputOmitted> ex)
                {
                    lblCustomerSaved.Text = ex.Detail.Message + " " + ex.Detail.Description;
                }
                catch (FaultException exc)
                {
                    lblCustomerSaved.Text = exc.Message;
                }
            }
            else
            {
                Response.Redirect("ErrorPageCustomer.aspx");
            }
        }

        protected void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            Customer c = GetCustomerIfValidId(txtId);
            if (null != c)
            {
                try
                {
                    client.DeleteCustomer(Convert.ToInt32(txtId.Value));
                    txtLastName.Text = "";
                    txtFirstName.Text = "";
                    txtPhoneNumber.Text = "";
                    txtEmailAddress.Text = "";
                    Response.Write("<script>alert('customer permanently deleted from system');</script>");
                }
                catch (FaultException<InvalidIdInput> ex)
                {
                    lblCustomerSaved.Text = ex.Detail.Message + " " + ex.Detail.Description;
                }
                catch (FaultException ex)
                {
                    lblCustomerSaved.Text = ex.Message;
                }
                catch (EndpointNotFoundException ex)
                {
                    lblCustomerSaved.Text = ex.Message;
                }             
            }
            else
            {
                Response.Redirect("ErrorPageCustomer.aspx");
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
                lblCustomerSaved.Text = "review input in customer-id text field, can't find customer.";
                return false;
            }
        }

        private Customer GetCustomerIfValidId(HtmlInputText id)
        {
            if (CheckIfTextFieldIsEmpty(id))
            {
                try
                {
                    Customer customer = client.GetCustomer(Convert.ToInt32(id.Value));
                    return customer;
                }
                catch (FaultException<EntityNotFound> ex)
                {
                    lblCustomerSaved.Text = ex.Detail.Description + ", " + ex.Detail.Message;
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