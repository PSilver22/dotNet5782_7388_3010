using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
	namespace DO
	{
		class InvalidIdException : Exception 
		{
			public InvalidIdException() : base() { }

			public InvalidIdException(string message) : base("Invalid ID Exception: " + message) { }

			public InvalidIdException(string message, Exception innerException) : base("Invalid ID Exception: " + message, innerException) { }

			public InvalidIdException(int id) : this($"{id} is not valid.") { }
		}
	}
}
