﻿using System;
using IDAL.DO;

namespace IBL.BO
{
    public class PackageInCustomer
    {
        public int Id { get; init; }
        public IDAL.DO.WeightCategory Weight { get; init; }
        public IDAL.DO.Priority Priority { get; init; }
        public PackageStatus Status { get; init; }
        public PackageCustomer Customer { get; init; }

        public PackageInCustomer(int id, WeightCategory weight, Priority priority, PackageStatus status, PackageCustomer customer)
        {
            Id = id;
            Weight = weight;
            Priority = priority;
            Status = status;
            Customer = customer;
        }

        public override string ToString()
        {
            return $"  id: {Id}\n" +
                $"  weight: {Weight}\n" +
                $"  priority: {Priority}\n" +
                $"  status: {Status}\n" +
                $"  customer:\n{Customer}";
        }
    }
}