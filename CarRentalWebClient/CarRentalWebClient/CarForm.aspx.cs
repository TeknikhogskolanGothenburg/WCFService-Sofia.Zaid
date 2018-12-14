using CarRentalWebClient.CarRentalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalWebClient
{
    public partial class CarForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CarRentalServiceClient carRentalServiceClient = new CarRentalServiceClient("BasicHttpBinding_ICarRentalService");
            Label1.Text = carRentalServiceClient.Pickup(Int32.Parse(TextBox1.Text));
        }
    }
}