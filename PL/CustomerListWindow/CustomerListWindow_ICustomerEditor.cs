using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public partial class CustomerListWindow : ICustomerEditor
    {
        public void UpdateCustomer(int id, string name, string phone) {
            bl.UpdateCustomer(id, name, phone);
            ReloadCustomers();
        }

        public List<BL.PackageListing> GetDeliveredPackagesList(BL.Customer customer)
        {
            return bl.GetPackageList(p =>
            {
                var package = bl.GetPackage(p.Id);
                return package.Receiver.Id == customer.Id;
            });
        }

        public List<BL.PackageListing> GetSentPackagesList(BL.Customer customer) {
            return bl.GetPackageList(p =>
            {
                var package = bl.GetPackage(p.Id);
                return package.Sender.Id == customer.Id;
            });
        }
    }
}
