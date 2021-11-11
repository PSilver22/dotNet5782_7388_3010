using System;
using System.Linq;
using System.Collections.Generic;
using IDAL.DO;

namespace DalObject
{
	public partial class DalObject
	{
        /// <summary>
        /// Adds a customer to the list of customers
        /// </summary>
        /// <param name="customer">The customer to add</param>
        /// <returns>true if the customer was successfully added, false otherwise</returns>
        public void AddCustomer(Customer customer)
        {
            if (customer.Id < 0)
            {
                throw new InvalidIdException(customer.Id);
            }

            if (DataSource.customers.Count < DataSource.MaxCustomers)
            {
                DataSource.customers.Add(customer);
            }

            throw new MaximumCapacityException("Customer list is at max capacity.");
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
        /// Creates a string with the information for every customer in the customers list
        /// </summary>
        /// <returns>
        /// string with the information for every customer
        /// </returns>
        public string GetCustomerList()
        {
            return ListItems<Customer>(DataSource.customers);
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
