#nullable enable
using DalApi;
using System;
using System.Collections.Generic;
using DO;
using DalObject;

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
