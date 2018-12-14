using CarRental.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CarRentalRestService
{
    [ServiceContract(Namespace = "http://sofiazaid.net/carRental/2018/11", Name = "ICarRentalRestService")]
    public interface ICarRentalRestService
    {
        /// <summary>
        /// Operation that gets all customers from db.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "Customers",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<Customer> GetAllCustomers();

        /// <summary>
        /// Operation that adds one customer to db. The body is wrapped which
        /// means there are one extra pair of tags around the body. Could be
        /// useful in some client systems.
        /// </summary>
        /// <param name="customer">Customer data in the form of a customer object, 
        /// to add to db.</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
         UriTemplate = "AddCustomer",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        int AddCustomer(Customer customer);

        /// <summary>
        /// Operation that adds several customers to db.
        /// </summary>
        /// <param name="customers">Collection of customer data to add to db.</param>
        /// <returns>Collection of numbers representing customer ids of customers
        /// just added to db.</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "AddSeveralCustomers",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<int> AddSeveralCustomers(IEnumerable<Customer> customers);

        /// <summary>
        /// Operation that adds a car to db.
        /// </summary>
        /// <returns>Number representing the id of newly added car.</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "AddCar",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        int AddCar();

        /// <summary>
        /// Operation that edits an existing customer object. 
        /// A customer object is sent in (data representing 
        /// values of the properties of a customer object)
        /// as well as an id to identify which customer we 
        /// want to edit.
        /// </summary>
        /// <param name="customer">The customer object.</param>
        /// <param name="id">The id of the customer object to edit.</param>
        [OperationContract]
        [WebInvoke(Method = "PUT",
           UriTemplate = "EditCustomer/{id}",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        void ChangeCustomer(Customer customer, string id);

        /// <summary>
        /// Operation that deletes customer with given id from db.
        /// </summary>
        /// <param name="id">ID of customer to delete.</param>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
           UriTemplate = "DeleteCustomer/{id}",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        void DeleteCustomer(string id);
    }
}
