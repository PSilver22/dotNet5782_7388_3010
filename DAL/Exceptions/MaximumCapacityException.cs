using System;

namespace IDAL
{
	namespace DO
	{
		class MaximumCapacityException : Exception
		{
			public MaximumCapacityException() : base() { }

			public MaximumCapacityException(string message) : base("Maximum Capacity Exception: " + message) { }

			public MaximumCapacityException(string message, Exception innerException) : base("Maximum Capacity Exception: " + message, innerException) { }
		}
	}
}
