using CarRentalWebClient.CarRentalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalWebClient
{
    public partial class AddCarForm : System.Web.UI.Page
    {
        CarService client;
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
   
        protected void btnSaveNewCar_Click(object sender, EventArgs e)
        {
            try
            {
                Car car = CreateNewCar();
                if (client.GetCarByRegNr(car.RegNumber = txtRegNr.Value) != null)
                {
                    Response.Redirect("ErrorPageCar.aspx");
                }
                else
                {
                    var theCarId = client.AddCar(car);
                    lblCarSaved.InnerText = "Car saved";
                    txtRegNr.Value = string.Empty;
                    txtModel.Value = string.Empty;
                    txtManYear.Value = string.Empty;
                    txtBrand.Value = string.Empty;
                }
            }
            catch (FaultException<RequiredInputOmitted> ex)
            {
                lblNotableToSaveCar.Text = "Something went wrong when trying to save new car\n. " + ex.Detail.Message + " "
                    + ex.Detail.Description;
            }
            catch (EndpointNotFoundException ex)
            {
                lblNotableToSaveCar.Text = ex.Message;
            }            
        }

        private Car CreateNewCar()
        {
            Car car = new Car
            {
                Brand = txtBrand.Value,
                Model = txtModel.Value,
                ManuFacturingYear = Convert.ToInt32(txtManYear.Value),
                RegNumber = txtRegNr.Value,

            };
            return car;
        }
    }
}