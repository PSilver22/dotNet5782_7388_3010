using DalApi;
using System;
using System.IO;
using System.Xml.Linq;

namespace DalXML {
    public partial class DalXml : IDAL {
        private static readonly Lazy<DalXml> instance = new(() => new DalXml());

        /// <summary>
        /// The Instance for the singleton
        /// </summary>
        public static DalXml Instance { 
            get {
                return instance.Value;
            }
        }

        /// <summary>
        /// Initializes DataSource
        /// </summary>
        private DalXml() {
            Utilities.DataSourceXml.Initialize();
        }

        
    }
}
