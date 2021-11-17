using System;
using IDAL.DO;

namespace IBL.BO
{
    public class PackageInTransfer
    {
        public int Id { get; init; }
        public IDAL.DO.WeightCategory Weight { get; init; }
        public IDAL.DO.Priority Priority { get; init; }
        public bool OutForDelivery { get; init; }
        public PackageCustomer Sender { get; init; }
        public PackageCustomer Receiver { get; init; }
        public Location CollectionLoc { get; init; }
        public Location DeliveryLoc { get; init; }
        public double DeliveryDistance { get; init; }

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

        public override string ToString()
        {
            return $"  id: {Id}\n" +
                $"  weight: {Weight}\n" +
                $"  priority: {Priority}\n" +
                $"  out for delivery: {OutForDelivery}\n" +
                $"  sender:\n{Sender}\n" +
                $"  receiver:\n{Receiver}\n" +
                $"  collection location: {CollectionLoc}\n" +
                $"  delivery location: {DeliveryLoc}\n" +
                $"  delivery distance: {DeliveryDistance}";
        }
    }
}
