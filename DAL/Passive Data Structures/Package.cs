#nullable enable

using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// Structure containing package information
        /// </summary>
        public struct Package : IIdentifiable
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategory Weight { get; set; }
            public Priority Priority { get; set; }
            public DateTime Requested { get; set; }
            public int? DroneId { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }

            public Package(
                int id,
                int senderId,
                int targetId,
                WeightCategory weight,
                Priority priority,
                DateTime requested,
                int? droneId,
                DateTime? scheduled,
                DateTime? pickedUp,
                DateTime? delivered)
            {
                Id = id;
                SenderId = senderId;
                TargetId = targetId;
                Weight = weight;
                Priority = priority;
                Requested = requested;
                DroneId = droneId;
                Scheduled = scheduled;
                PickedUp = pickedUp;
                Delivered = delivered;
            }

            /// <summary>
            /// Creates a string with the package information
            /// </summary>
            /// <returns>
            /// String with the package information
            /// </returns>
            public override string ToString()
            {
                return
                    $"Parcel: {Id}\n" +
                    $"Sender ID: {SenderId}\n" +
                    $"Target ID: {TargetId}\n" +
                    $"Weight class: {Weight.ToString()}\n" +
                    $"Priority: {Priority.ToString()}\n" +
                    $"Date requested: {Requested.ToLongDateString()}\n" +
                    $"Drone ID: {DroneId}\n" +
                    $"Scheduled date: {Scheduled?.ToLongDateString() ?? "N/A"}\n" +
                    $"Pick up date: {PickedUp?.ToLongDateString() ?? "N/A"}\n" +
                    $"Delivered date: {Delivered?.ToLongDateString() ?? "N/A"}\n";
            }
        }
    }
}
