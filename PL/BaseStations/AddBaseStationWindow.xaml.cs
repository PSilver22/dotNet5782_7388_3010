#nullable enable

using System;
using System.Windows;
using System.Windows.Input;
using BlApi;
using MapControl;
using PL.Utilities;

namespace PL
{
    public partial class AddBaseStationWindow : Window
    {
        private IBL bl;

        public Prop<int?> Id { get; set; } = new();
        public Prop<string> StationName { get; set; } = new();
        public Prop<ushort> NumChargingSlots { get; set; } = new();

        public Prop<Location> Loc { get; } = new()
        {
            Value = new Location(31.765, 35.1906)
        };

        /// <summary>
        /// Called when a station is added
        /// </summary>
        public event Action<int>? StationAdded;

        public AddBaseStationWindow(IBL bl)
        {
            this.bl = bl;

            InitializeComponent();
        }

        private void MapClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Loc.Value = Map.ViewToLocation(e.GetPosition(Map));
            }
        }

        private void AddStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddBaseStation(Id.Value!.Value, StationName.Value, Loc.Value.Latitude, Loc.Value.Longitude,
                    NumChargingSlots.Value);
                Close();
                StationAdded?.Invoke(Id.Value!.Value);
            }
            catch (DO.DuplicatedIdException)
            {
                MessageBox.Show("The selected ID is already in use.",
                    "Duplicated ID",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}