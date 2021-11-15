using System;
using IDAL.DO;

namespace IBL.BO
{
    public class PackageListing
    {
        int Id { get; init; }
        string SenderName { get; init; }
        string ReceiverName { get; init; }
        IDAL.DO.WeightCategory Weight { get; init; }
        IDAL.DO.Priority Priority { get; init; }
        PackageStatus Status { get; init; }

        public PackageListing(int id, string senderName, string receiverName, WeightCategory weight, Priority priority, PackageStatus status)
        {
            Id = id;
            SenderName = senderName;
            ReceiverName = receiverName;
            Weight = weight;
            Priority = priority;
            Status = status;
        }
    }
}
