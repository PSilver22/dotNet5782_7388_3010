using System;
using IDAL.DO;

namespace BL
{
    public class PackageListing
    {
        public int Id { get; init; }
        public string SenderName { get; init; }
        public string ReceiverName { get; init; }
        public WeightCategory Weight { get; init; }
        public Priority Priority { get; init; }
        public PackageStatus Status { get; init; }

        public PackageListing(int id, string senderName, string receiverName, WeightCategory weight, Priority priority, PackageStatus status)
        {
            Id = id;
            SenderName = senderName;
            ReceiverName = receiverName;
            Weight = weight;
            Priority = priority;
            Status = status;
        }

        public override string ToString()
        {
            return $"id: {Id}\n" +
                $"sender:\n{SenderName}\n" +
                $"receiver:\n{ReceiverName}\n" +
                $"weight: {Weight}\n" +
                $"priority: {Priority}\n" +
                $"status: {Status}";
        }
    }
}
