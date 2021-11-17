using System;
using System.Collections.Generic;

namespace IBL.BO
{
    public class Customer
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Phone { get; init; }
        public Location Location { get; init; }
        public List<PackageInCustomer> SentPackages { get; init; }
        public List<PackageInCustomer> ReceivingPackages { get; init; }

        public Customer(int id, string name, string phone, Location location, List<PackageInCustomer> sentPackages, List<PackageInCustomer> receivingPackages)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Location = location;
            SentPackages = sentPackages;
            ReceivingPackages = receivingPackages;
        }

        public override string ToString()
        {
            return $"id: {Id}\n" +
                $"name: {Name}\n" +
                $"phone number: {Phone}\n" +
                $"location: {Location}\n" +
                $"sent packages: " + String.Join("\n", SentPackages) + "\n" +
                $"expected packages: " + String.Join("\n", ReceivingPackages);
        }
    }
}
