using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    /// <summary>
    /// Factory for IBL
    /// </summary>
    public static class BLFactory
    {
        /// <summary>
        /// Creates an instance of IBL
        /// </summary>
        /// <returns>Instance of IBL</returns>
        public static IBL GetBl() {
            return BL.Instance;
        }
    }
}
