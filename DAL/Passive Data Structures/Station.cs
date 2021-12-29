
namespace DO
{
    /// <summary>
    /// Struct containing the information of a Station
    /// </summary>
    public struct Station : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public int ChargeSlots { get; set; }

        public Station(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            Id = id;
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
            ChargeSlots = chargeSlots;
        }

        /// <summary>
        /// Creates a string with the station info
        /// </summary>
        /// <returns>
        ///	Returns the string with the station info
        /// </returns>
        public override string ToString()
        {
            return
                $"Station: {Id}\n" +
                $"Name: {Name}\n" +
                $"Longitude: {FormatUtils.Coordinates.Sexagesimal(Longitude, FormatUtils.Coordinates.Axis.longitude)}\n" +
                $"Latitude: {FormatUtils.Coordinates.Sexagesimal(Latitude, FormatUtils.Coordinates.Axis.latitude)}\n" +
                $"Charge slots: {ChargeSlots}\n";
        }
    }
}
