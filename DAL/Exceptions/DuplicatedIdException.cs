using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
	namespace DO
	{
		public class DuplicatedIdException : Exception
		{
			public DuplicatedIdException(int id, string dataType) : base($"The {dataType} with {id} already exists in the database.") { }
		}
	}
}
