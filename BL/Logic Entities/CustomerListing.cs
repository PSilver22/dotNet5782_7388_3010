using System;
namespace BL
{
    public class CustomerListing
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Phone { get; init; }
        public int DeliveredPackageCount { get; init; }
        public int UndeliveredPackageCount { get; init; }
        public int ReceivedPackageCount { get; init; }
        public int ExpectedPackageCount { get; init; }

        public CustomerListing(int id, string name, string phone, int deliveredPackageCount, int undeliveredPackageCount, int receivedPackageCount, int expectedPackageCount)
        {
            Id = id;
            Name = name;
            Phone = phone;
            DeliveredPackageCount = deliveredPackageCount;
            UndeliveredPackageCount = undeliveredPackageCount;
            ReceivedPackageCount = receivedPackageCount;
            ExpectedPackageCount = expectedPackageCount;
        }

        public override string ToString()
        {
            return $"id: {Id}\n" +
                $"name: {Name}\n" +
                $"phone number: {Phone}\n" +
                $"delivered packages: {DeliveredPackageCount}\n" +
                $"undelivered packages: {UndeliveredPackageCount}\n" +
                $"received packages: {ReceivedPackageCount}\n" +
                $"expected packages: {ExpectedPackageCount}";
        }
    }
}
