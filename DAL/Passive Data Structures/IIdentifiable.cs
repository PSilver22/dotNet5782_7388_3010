using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
	namespace DO
	{
		/// <summary>
		/// Marks an object as having an ID
		/// </summary>
		public interface IIdentifiable
		{
			public int Id { get; set; }
		}
	}
}
