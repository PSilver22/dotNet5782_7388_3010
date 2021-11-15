using System;
namespace IBL.BO
{
    public class PackageCustomer
    {
        int Id { get; init; }
        string Name { get; init; }

        public PackageCustomer(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
