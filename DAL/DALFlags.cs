using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public enum WeightCategory { heavy = 3, medium = 2, light = 1 };

        public enum DroneStatus { free, maintenance, delivery };

        public enum Priority { regular, fast, emergency };
    }
}
