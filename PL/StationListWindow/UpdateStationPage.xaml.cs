using System.Windows;
using System.Windows.Controls;
using BL;
using PL.Utilities;
using System;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateStationPage.xaml
    /// </summary>
    public partial class UpdateStationPage : Page
    {
        public IStationEditor Delegate { get; set; }
        private BaseStation station;
        
        public BaseStation Station {
            get => station;
        }

        /// <summary>
        /// Setter for station
        /// </summary>
        /// <param name="station">station to update with</param>
        /// <param name="notify">If true the method will update it's observer</param>
        public void SetStation(BaseStation station, bool notify = true) {
            this.station = station;
            UpdateStationInfo(notify);
        }

        public DelegateCommand UpdateStationCommand { get; }

        /// <summary>
        /// Constructor for UpdateStationPage
        /// </summary>
        /// <param name="itsWindow">The window that the page is in</param>
        /// <param name="station">The station to display</param>
        public UpdateStationPage(IStationEditor itsWindow, BaseStation station)
        {
            InitializeComponent();

            this.station = station;
            Delegate = itsWindow;
            UpdateStationInfo();

            UpdateStationCommand = new DelegateCommand((_) => UpdateCurrentStation());
        }
        
        /// <summary>
        /// Updates the page with new information
        /// </summary>
        /// <param name="notify"></param>
        private void UpdateStationInfo(bool notify = true)
        {
            IdLabel.Content = station.Id;
            NameTextBox.Text = station.Name;
            AvailableSlotsLabel.Content = station.AvailableChargingSlots;
            TotalSlotsTextBox.Text = (station.AvailableChargingSlots + station.ChargingDrones.Count).ToString();

            ChargingDronesListView.ItemsSource = station.ChargingDrones;

            if (notify)
            {
                NotifyObservers();
            }

            ApplyButton.IsEnabled = false;
        }

        /// <summary>
        /// Click event for apply button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateCurrentStation();
        }

        /// <summary>
        /// Updates the station data with the user inputted data
        /// </summary>
        private void UpdateCurrentStation() {
            if (!int.TryParse(TotalSlotsTextBox.Text, out int totalSlotCount))
            {
                MessageBox.Show("ERROR: Total charge slot input is invalid.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (totalSlotCount < 2 * station.ChargingDrones.Count)
            {
                MessageBox.Show("ERROR: Can't have total slot count less than the amount of charging drones.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                Delegate.UpdateStation(station.Id, NameTextBox.Text, totalSlotCount - station.ChargingDrones.Count);
            }
        }

        /// <summary>
        /// Text changed event for NameTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyButton.IsEnabled = NameTextBox.Text != station.Name;
        }

        /// <summary>
        /// Text changed event for TotalSlotsTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSlotsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyButton.IsEnabled = TotalSlotsTextBox.Text != station.AvailableChargingSlots.ToString();
        }

        /// <summary>
        /// Mouse double click event for ChargingDronesListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChargingDronesListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BlApi.IBL bl = BlApi.BLFactory.GetBl();
            DroneListWindow droneWindow = new DroneListWindow(bl);
            droneWindow.SelectDrone(((ChargingDrone)ChargingDronesListView.SelectedItem).Id);

            droneWindow.Show();
        }
    }
}
