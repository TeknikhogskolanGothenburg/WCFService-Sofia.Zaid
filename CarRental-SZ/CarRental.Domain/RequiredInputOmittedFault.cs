using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain
{
    [DataContract(Namespace = "http://sofiazaid.net/carRental/2018/11", Name = "RequiredInputOmitted")]
    public class RequiredInputOmittedFault
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
