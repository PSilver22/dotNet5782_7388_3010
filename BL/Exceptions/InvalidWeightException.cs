using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
	public class InvalidWeightException : Exception
	{
		public InvalidWeightException() : base("The given weight is invalid.") { }
	}
}
