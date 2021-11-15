using System;
using IDAL.DO;

namespace IBL.BO
{
    public class PackageInCustomer
    {
        int Id { get; init; }
        IDAL.DO.WeightCategory Weight { get; init; }
        IDAL.DO.Priority Priority { get; init; }
        PackageStatus Status { get; init; }
        PackageCustomer Customer { get; init; }

        public PackageInCustomer(int id, WeightCategory weight, Priority priority, PackageStatus status, PackageCustomer customer)
        {
            Id = id;
            Weight = weight;
            Priority = priority;
            Status = status;
            Customer = customer;
        }
    }
}
