using System;
using System.Linq;
using System.Collections.Generic;
using IDAL.DO;

namespace DalObject
{
    /// <summary>
    /// A class that interacts with and performs operations
    /// on the data source class
    /// </summary>
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Initializes DataSource
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        /// Gets index of item from list with ID equal to key
        /// </summary>
        /// <param name="key">Key to search for</param>
        /// <param name="list">List to search within</param>
        /// <returns>element with ID equal to key. throws if key isn't found</returns>
        private static int GetItemIndexByKey<T>(int key, List<T> list) where T : IIdentifiable
        {
            if (key < 0)
            {
                throw new InvalidIdException(key);
            }

            // search for element in array with id equal to key
            for (int index = 0; index < list.Count; ++index)
            {
                T element = list[index];

                if (element.Id == key)
                {
                    return index;
                }
            }

            // if an element with id is not found, throw an exception
            // These should really be classes...
            throw new IdNotFoundException(key);
        }

        /// <summary>
        /// Gets item from list with ID equal to key
        /// </summary>
        /// <param name="key">Key to search for</param>
        /// <param name="list">List to search within</param>
        /// <returns>element with ID equal to key. throws if key isn't found</returns>
        private static T GetItemByKey<T>(int key, List<T> list) where T : IIdentifiable
        {
            return list[GetItemIndexByKey<T>(key, list)];
        }

        /// <summary>
        /// Creates a string that lists the elements of array
        /// </summary>
        /// <param name="array">Array to create list from</param>
        /// <returns>String that lists the elements of array</returns>
        private static string ListItems<T>(List<T> list) where T : struct
        {
            // concatenate every element in array into a list
            string result = "";
            for (int index = 0; index < list.Count; ++index)
            {
                T element = list[index];

                result += element.ToString() + "\n";
            }

            return result;
        }
    }
}