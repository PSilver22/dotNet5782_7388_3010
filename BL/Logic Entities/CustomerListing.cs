using System;
namespace IBL.BO
{
    public class CustomerListing
    {
        int Id { get; init; }
        string Name { get; init; }
        string Phone { get; init; }
        int DeliveredPackageCount { get; init; }
        int UndeliveredPackageCount { get; init; }
        int ReceivedPackageCount { get; init; }
        int ExpectedPackageCount { get; init; }
    }
}
