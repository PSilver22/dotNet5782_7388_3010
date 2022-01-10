using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public interface ICustomerEditor
    {
        public void UpdateCustomer(int customerId, string name, string phoneNum);

        public List<BL.PackageListing> GetDeliveredPackagesList(BL.Customer customer);

        public List<BL.PackageListing> GetSentPackagesList(BL.Customer customer);
    }
}
