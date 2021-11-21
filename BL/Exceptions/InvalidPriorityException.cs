using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
	public class InvalidPriorityException : Exception
	{
		public InvalidPriorityException() : base("The given priority is invalid.") { }
	}
}
