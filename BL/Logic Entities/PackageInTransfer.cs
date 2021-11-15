using System;
using IDAL.DO;

namespace IBL.BO
{
    public class PackageInTransfer
    {
        int Id { get; init; }
        IDAL.DO.WeightCategory Weight { get; init; }
        IDAL.DO.Priority Priority { get; init; }
        bool OutForDelivery { get; init; }
        PackageCustomer Sender { get; init; }
        PackageCustomer Receiver { get; init; }
        Location CollectionLoc { get; init; }
        Location DeliveryLoc { get; init; }
        double DeliveryDistance { get; init; }

        public PackageInTransfer(int id,
                                 WeightCategory weight,
                                 Priority priority,
                                 bool outForDelivery,
                                 PackageCustomer sender,
                                 PackageCustomer receiver,
                                 Location collectionLoc,
                                 Location deliveryLoc,
                                 double deliveryDistance)
        {
            Id = id;
            Weight = weight;
            Priority = priority;
            OutForDelivery = outForDelivery;
            Sender = sender;
            Receiver = receiver;
            CollectionLoc = collectionLoc;
            DeliveryLoc = deliveryLoc;
            DeliveryDistance = deliveryDistance;
        }
    }
}
