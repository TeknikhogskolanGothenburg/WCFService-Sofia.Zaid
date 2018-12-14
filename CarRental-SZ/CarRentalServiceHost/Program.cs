using System;
using System.ServiceModel;
using CarRentalService;
using CarRentalRestService;
using System.ServiceModel.Web;
using System.ServiceModel.Description;

namespace CarRentalServiceHost
{
    class Program
    {
        static void Main()
        {
            WebServiceHost restHost = new WebServiceHost(typeof(CarRentalRestService.CarRentalRestService));

            //using statements: running two separate host instances for the two separate services CarRentalService and CarRentalRestService.
            using (ServiceHost host = new ServiceHost(typeof(CarRentalService.CarRentalService)))
            using (restHost)
            {
                host.Open();
                Console.WriteLine("host started  @ " + DateTime.Now.ToString());

                restHost.Open();
                Console.WriteLine("Rest host is started");
                Console.ReadLine();
            }
        }
    }
}
