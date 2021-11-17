#nullable enable

using System;
using IDAL.DO;

namespace IBL.BO
{
    public class Package
    {
        public int Id { get; init; }
        public PackageCustomer Sender { get; init; }
        public PackageCustomer Receiver { get; init; }
        public IDAL.DO.WeightCategory Weight { get; init; }
        public IDAL.DO.Priority Priority { get; init; }
        public DroneInDelivery? Drone { get; init; }
        public DateTime CreationTime { get; init; }
        public DateTime? AssignmentTime { get; init; }
        public DateTime? CollectionTime { get; init; }
        public DateTime? DeliveryTime { get; init; }

        public Package(int id,
                       PackageCustomer sender,
                       PackageCustomer receiver,
                       WeightCategory weight,
                       Priority priority,
                       DroneInDelivery? drone,
                       DateTime creationTime,
                       DateTime? assignmentTime,
                       DateTime? collectionTime,
                       DateTime? deliveryTime)
        {
            Id = id;
            Sender = sender;
            Receiver = receiver;
            Weight = weight;
            Priority = priority;
            Drone = drone;
            CreationTime = creationTime;
            AssignmentTime = assignmentTime;
            CollectionTime = collectionTime;
            DeliveryTime = deliveryTime;
        }

        public override string ToString()
        {
            return $"id: {Id}\n" +
                $"sender:\n{Sender}\n" +
                $"receiver:\n{Receiver}\n" +
                $"weight: {Weight}\n" +
                $"priority: {Priority}\n" +
                $"drone:\n{Drone}\n" +
                $"creation time: {CreationTime}\n" +
                $"assignment time: {AssignmentTime}\n" +
                $"collection time: {CollectionTime}\n" +
                $"delivery time: {DeliveryTime}";
        }
    }
}
