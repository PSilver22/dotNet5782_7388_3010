using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException(int id) : base($"The ID {id} is invalid.") {}
    }
}
