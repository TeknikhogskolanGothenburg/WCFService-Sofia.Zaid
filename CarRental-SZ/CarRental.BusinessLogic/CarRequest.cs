using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;

namespace CarRental.BusinessLogic
{
    /// <summary>
    /// In this class we define a message contract concerning how request messages regarding car objects should 
    /// be structured. We define that a request should contain a message header with a licensekey and
    /// a message body with a car id. Correct licensekey needs to be provided in the request to get the
    /// response asked for from the service.
    /// </summary>
    [MessageContract(IsWrapped = true,
    WrapperName = "CarRequestObject",
    WrapperNamespace = "http://sofiazaid.net/carRental/car/2018/11")]
    public class CarRequest
    {
        [MessageHeader(Namespace = "http://sofiazaid.net/carRental/car/2018/11")]

        public string LicenseKey { get; set; }

        [MessageBodyMember(Namespace = "http://sofiazaid.net/carRental/car/2018/11")]
        public int CarId { get; set; }
    }
}
