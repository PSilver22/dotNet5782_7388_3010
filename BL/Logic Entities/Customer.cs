using System;
using System.Collections.Generic;

namespace IBL.BO
{
    public class Customer
    {
        int Id { get; init; }
        string Name { get; init; }
        string Phone { get; init; }
        Location Location { get; init; }
        List<PackageInCustomer> SentPackages { get; init; }
        List<PackageInCustomer> ExpectedPackages { get; init; }

        public Customer(int id, string name, string phone, Location location, List<PackageInCustomer> sentPackages, List<PackageInCustomer> expectedPackages)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Location = location;
            SentPackages = sentPackages;
            ExpectedPackages = expectedPackages;
        }
    }
}
