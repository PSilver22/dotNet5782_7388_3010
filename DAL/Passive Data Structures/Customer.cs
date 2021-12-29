namespace DO
{
    /// <summary>
    /// Structure with information about a customer
    /// </summary>
    public struct Customer : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Customer(int id, string name, string phone, double longitude, double latitude)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>
        /// Creates a string with the customer's info
        /// </summary>
        /// <returns>
        /// String with customer's info
        /// </returns>
        public override string ToString()
        {
            return
                $"Customer {Id}:\n" +
                $"Name: {Name}\n" +
                $"Phone: {Phone}\n" +
                $"Longitude: {FormatUtils.Coordinates.Sexagesimal(Longitude, FormatUtils.Coordinates.Axis.longitude)}\n" +
                $"Latitude: {FormatUtils.Coordinates.Sexagesimal(Latitude, FormatUtils.Coordinates.Axis.latitude)}\n";
        }
    }
}

