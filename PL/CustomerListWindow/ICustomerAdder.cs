using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public interface ICustomerAdder
    {
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
    }
}
