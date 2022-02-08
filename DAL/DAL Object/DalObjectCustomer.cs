#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using DO;
using DalApi;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// Adds a customer to the list of customers
        /// </summary>
        /// <param name="customer">The customer to add</param>
        public void AddCustomer(Customer customer)
        {
            if (customer.Id < 0)
            {
                throw new InvalidIdException(customer.Id);
            }

            if (DataSource.customers.Exists(c => c.Id == customer.Id))
            {
                throw new DuplicatedIdException(customer.Id, "customer");
            }

            DataSource.customers.Add(customer);
        }

        /// <summary>
        /// gets a customer by id from the customers array
        /// </summary>
        /// <param name="id">id of the customer</param>
        /// <returns>Customer with the given id if found. null otherwise</returns>
        public Customer GetCustomer(int id)
        {
            return GetItemByKey<Customer>(id, DataSource.customers);
        }

        /// <summary>
        /// Returns a list with the information for every customer in the customers list
        /// </summary>
        /// <param name="filter">The filter applied to the objects in the list</param>
        /// <returns>
        /// Customer list
        /// </returns>
        public IEnumerable<Customer> GetCustomerList(Predicate<Customer>? filter = null)
        {
            return DataSource.customers.Where(new Func<Customer, bool>(filter ?? (x => true))).ToList();
        }

        /// <summary>
        /// Updates the customer with given id to the given values
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public void UpdateCustomer(int id, string? name = null, string? phone = null, double? longitude = null, double? latitude = null)
        {
            int index = GetCustomerIndex(id);

            Customer updatedCustomer = DataSource.customers[index];

            updatedCustomer.Name = name ?? updatedCustomer.Name;
            updatedCustomer.Phone = phone ?? updatedCustomer.Phone;
            updatedCustomer.Longitude = longitude ?? updatedCustomer.Longitude;
            updatedCustomer.Latitude = latitude ?? updatedCustomer.Latitude;

            SetCustomer(updatedCustomer);
        }

        /// <summary>
        /// Sets the customer with matching id to the given customer
        /// </summary>
        /// <param name="customer"></param>
        public void SetCustomer(Customer customer)
        {
            int index = GetCustomerIndex(customer.Id);

            DataSource.customers[index] = customer;
        }

        /// <summary>
        /// gets a package's index by id from the packages array
        /// </summary>
        /// <param name="id">id of the package</param>
        /// <returns>Index of the package with the given id if found.</returns>
        private int GetCustomerIndex(int id)
        {
            return GetItemIndexByKey<Customer>(id, DataSource.customers);
        }
    }
}
