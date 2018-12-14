using CarRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net.Security;

namespace CarRentalService
{
    [ServiceContract(Namespace = "http://sofiazaid.net/carRental/2018/11", Name = "CustomerService")]
    public interface ICustomerService
    {
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(EntityNotFoundFault))]
        Customer GetCustomer(int id);

        [OperationContract(ProtectionLevel = ProtectionLevel.Sign)]
        [FaultContract(typeof(DbUpdateFault))]
        [FaultContract(typeof(RequiredInputOmittedFault))]
        int AddNewCustomer(Customer customer);

        [OperationContract(ProtectionLevel = ProtectionLevel.Sign)]
        [FaultContract(typeof(DbUpdateFault))]
        [FaultContract(typeof(RequiredInputOmittedFault))]
        void ChangeCustomer(Customer customer);

        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(InvalidIdInputFault))]
        void DeleteCustomer(int id);
    }
}
