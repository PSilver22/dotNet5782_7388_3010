using System;
using DalApi;

namespace DalApi
{
    /// <summary>
    /// Factory for IDAL
    /// </summary>
    public static class DalFactory
    {
        /// <summary>
        /// Gets an instance of IDAL
        /// </summary>
        /// <returns>Instance of IDAL</returns>
        public static IDAL GetDAL(string type)
        {
            return type switch
            {
                "Object" => DalObject.DalObject.Instance,
                "xml" => DalXML.DalXml.Instance,
                _ => throw new InvalidDalTypeException(type),
            };
        }
    }
}
