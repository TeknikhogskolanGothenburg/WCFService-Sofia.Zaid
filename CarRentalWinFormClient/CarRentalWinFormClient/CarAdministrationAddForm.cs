using CarRentalWinFormClient.CarRentalService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalWinFormClient
{
    public partial class CarAdministrationAddForm : Form
    {
        private CarServiceClient client = new CarServiceClient("NetTcpBinding_CarService");
        public CarAdministrationAddForm()
        {
            InitializeComponent();

            Size = new Size(500, 500);
            AutoSizeMode = AutoSizeMode;

        }

        private void btnSaveCar_Click(object sender, EventArgs e)
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

        private Car CreateNewCar()
        {
            Car car = new Car
            {
                Brand = txtBrand.Text,
                Model = txtModel.Text,
                ManuFacturingYear = Convert.ToInt32(txtManYear.Text),
                RegNumber = txtRegNr.Text

            };
            return car;
        }
    }
}
