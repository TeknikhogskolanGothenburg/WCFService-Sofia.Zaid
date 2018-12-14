using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Xml;
using CarRental.BusinessLogic;
using CarRental.Domain;

namespace CarRentalRestService
{
    public class CarRentalRestService : ICarRentalRestService
    {
        private APIMethods api = new APIMethods();

        /// <summary>
        /// Get list of all customers in db.
        /// </summary>
        /// <returns>A list with all customers in db (at the time of method call).</returns>
        public List<Customer> GetAllCustomers()
        {
            return api.GetCustomers();
        }

        /// <summary>
        /// Add one customer to db.
        /// </summary>
        /// <param name="customer">The customer object containing customer data to add to db.</param>
        /// <returns>An int representing the id of the customer that was added to db.</returns>
        public int AddCustomer(Customer customer)
        {
            return api.AddCustomer(customer);
        }

        /// <summary>
        /// Adds several customers to db 
        /// by operating on IEnumerable (collection
        /// of customer objects and calling method 
        /// "AddCustomer" on each of the objects.
        /// </summary>
        /// <param name="customers">Customer objects to add to db.</param>
        /// <returns>An IEnumerable (collection) representing the id's of customers 
        /// added to db.</returns>
        public IEnumerable<int> AddSeveralCustomers(IEnumerable<Customer> customers) =>
            customers.Select(c => api.AddCustomer(c));

        /// <summary>
        /// Add one car to db by turning JSON into XML,
        /// looping through each node in XML document to check for values
        /// to assign to Car object properties.
        /// </summary>
        /// <returns>A number representing the id of the newly created car object.</returns>
        public int AddCar()
        {
            Car car = new Car();
            string xmlString = OperationContext.Current.RequestContext.RequestMessage.ToString();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                switch (node.Name)
                {
                    case (nameof(Car.Model)):
                        car.Model = node.InnerText;
                        break;
                    case (nameof(Car.ManufacturingYear)):
                        car.ManufacturingYear = Convert.ToInt32(node.Value);
                        break;
                    case (nameof(Car.RegistrationNo)):
                        car.RegistrationNo = node.InnerText;
                        break;
                    case (nameof(Car.Brand)):
                        car.Brand = node.InnerText;
                        break;
                    default:
                        break;
                }
            }
            return api.AddCar(car);
        }

        public void ChangeCustomer(Customer customer, string id)
        {
            customer.Id = Convert.ToInt32(id);
            api.EditCustomer(customer);
        }

        public void DeleteCustomer(string id)
        {
            api.DeleteCustomer((Convert.ToInt32(id)));
        }
    }
}

