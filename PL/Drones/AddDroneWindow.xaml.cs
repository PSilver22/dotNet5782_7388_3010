#nullable enable

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BL;
using BlApi;
using DO;
using PL.Utilities;

namespace PL
{
    public partial class AddDroneWindow : Window
    {
        private IBL bl;

        public ObservableCollection<ComboBoxItem> Stations { get; set; }

        public Prop<int?> Id { get; set; } = new();
        public Prop<string> Model { get; set; } = new();
        public Prop<WeightCategory> Weight { get; set; } = new();
        public Prop<int?> StationId { get; set; } = new();

        /// <summary>
        /// Called when a drone is added
        /// </summary>
        public event Action<int>? DroneAdded;

        public AddDroneWindow(IBL bl)
        {
            this.bl = bl;

            Stations = new ObservableCollection<ComboBoxItem>(
                bl.GetBaseStationList()
                    .Select(s => new ComboBoxItem {Content = s.Name, Tag = s.Id}));

            InitializeComponent();
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddDrone(Id.Value!.Value, Model.Value,
                    Weight.Value, StationId.Value!.Value);
                Close();
                DroneAdded?.Invoke(Id.Value!.Value);
            }
            catch (NoAvailableChargingSlotsException)
            {
                MessageBox.Show("The selected base station has no available charging slots.",
                    "No Available Charging Slots",
                    MessageBoxButton.OK, MessageBoxImage.Error);
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