using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public enum WeightCategory { heavy = 2, medium = 1, light = 0 };

        public enum DroneStatus { free, maintenance, delivery };

        public enum Priority { regular, fast, emergency };
    }
}
