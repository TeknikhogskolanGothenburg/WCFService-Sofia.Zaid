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

    public partial class CarAdministrationDeleteForm : System.Web.UI.Page
    {
        private CarService client;
        private void CarClient_Faulted(object sender, EventArgs e)
        {
            client = Utility.CreateNewCarServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CarClient_Faulted);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            client = Utility.CreateNewCarServiceClient();
            ((ICommunicationObject)client).Faulted += new EventHandler(CarClient_Faulted);
        }

        protected void btnGetCar_Click(object sender, EventArgs e)
        {
            try
            {
                CarInfo car = GetCarInfoIfValidInput(txtId);
            }
            catch (FaultException ex)
            {
                lblLicenseKey.Text = ex.Message;
            }
        }
        protected void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                client.DeleteCar(Convert.ToInt32(txtId.Value));
                Response.Write("<script>alert('car deleted from system');</script>");
            }
            catch (FaultException<UpdateFail> ex)
            {
                lblMessageDelete.Text = ex.Detail.Message + " " + ex.Detail.Description;
                
            }
            catch (FaultException exc)
            {
                lblMessageDelete.Text = exc.Message;
            }
            txtBrand.Value = "";
            txtModel.Value = "";
            txtManYear.Value = "";
            txtRegNr.Value = "";
            txtId.Value = "";
            txtLicenseKey.Text = string.Empty;
        }

        private bool CheckIfTextFieldIsNotEmpty(HtmlInputText id)
        {
            int i = 0;
            var valid = int.TryParse(id.Value, out i) == true;
            if (id.Value != "" && valid == true)
            {
                return true;
            }
            else
            {
                lblMessageDelete.Text = "review input in carid-text field, can't find car.";
                return false;
            }
        }

        private CarInfo GetCarInfoIfValidInput(HtmlInputText id)
        {

            if (CheckIfTextFieldIsNotEmpty(id))
            {
                CarRequest request = new CarRequest();

                request.LicenseKey = txtLicenseKey.Text;
                request.CarId = Convert.ToInt32(txtId.Value);

                try
                {
                    CarInfo car = client.GetCarInfo(request);
                    txtBrand.Value = car.Brand;
                    txtManYear.Value = car.ManufacturingYear.ToString();
                    txtModel.Value = car.Model;
                    txtRegNr.Value = car.RegistrationNumber;
                    return car;
                }
                catch (FaultException ex)
                {
                    lblMessageDelete.Text = ex.Message;
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