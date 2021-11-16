namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// Structure with Drone information
        /// </summary>
        public struct Drone : IIdentifiable
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategory MaxWeight { get; set; }
            public double Battery { get; set; }

            public Drone(int id, string model, WeightCategory maxWeight, double battery)
            {
                Id = id;
                Model = model;
                MaxWeight = maxWeight;
                Battery = battery;
            }

            /// <summary>
            /// Creates a string with the drone's information
            /// </summary>
            /// <returns>
            ///	The string with drone's information
            /// </returns>
            public override string ToString()
            {
                return
                    $"Drone: {Id}\n" +
                    $"Model: {Model}\n" +
                    $"Max weight: {MaxWeight}\n" +
                    $"Battery: {Battery}\n";
            }
        }
    }
}
