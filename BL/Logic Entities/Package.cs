using System;
using IDAL.DO;

namespace IBL.BO
{
    public class Package
    {
        int Id { get; init; }
        PackageCustomer Sender { get; init; }
        PackageCustomer Receiver { get; init; }
        IDAL.DO.WeightCategory Weight { get; init; }
        IDAL.DO.Priority Priority { get; init; }
        DroneInDelivery Drone { get; init; }
        DateTime CreationTime { get; init; }
        DateTime AssignmentTime { get; init; }
        DateTime CollectionTime { get; init; }
        DateTime DeliveryTime { get; init; }

        public Package(int id,
                       PackageCustomer sender,
                       PackageCustomer receiver,
                       WeightCategory weight,
                       Priority priority,
                       DroneInDelivery drone,
                       DateTime creationTime,
                       DateTime assignmentTime,
                       DateTime collectionTime,
                       DateTime deliveryTime)
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
    }
}
