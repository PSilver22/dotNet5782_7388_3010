#nullable enable

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using BL;
using BlApi;
using PL.Utilities;

namespace PL
{
    public partial class UpdateDroneControl : UserControl
    {
        public int? DroneId
        {
            get => (int?) GetValue(DroneIdProperty);
            set => SetValue(DroneIdProperty, value);
        }

        public static readonly DependencyProperty DroneIdProperty =
            DependencyProperty.Register("DroneId", typeof(int?), typeof(UpdateDroneControl),
                new PropertyMetadata(null, (sender, _) => (sender as UpdateDroneControl)?.LoadDrone()));

        public Prop<Drone?> Drone { get; } = new();

        public Prop<PackageStatus?> PackageStatus { get; } = new();

        public DataManager Dm
        {
            get => (DataManager) GetValue(DmProperty);
            set => SetValue(DmProperty, value);
        }

        public static readonly DependencyProperty DmProperty =
            DependencyProperty.Register("Dm", typeof(DataManager), typeof(UpdateDroneControl));


        public Prop<string> NewModel { get; } = new();
        public DelegateCommand UpdateDroneCommand { get; }

        /// <summary>
        /// Display and interaction control for drones
        /// </summary>
        public UpdateDroneControl()
        {
            Loaded += OnLoaded;

            UpdateDroneCommand = new DelegateCommand(_ =>
            {
                if (Drone.Value?.Model == NewModel.Value) return;
                var id = DroneId!.Value;
                Dm.UpdateDrone(DroneId!.Value, NewModel.Value);
                DroneUpdated?.Invoke(this, id);
            });
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();

            Loaded -= OnLoaded;
        }

        private void LoadDrone()
        {
            Drone.Value = DroneId is null ? null : Dm.GetDrone(DroneId.Value);

            NewModel.Value = Drone.Value?.Model ?? string.Empty;

            if (Drone.Value?.Package is null)
            {
                PackageStatus.Value = null;
            }
            else
            {
                var package = Dm.GetPackage(Drone.Value.Package.Id);
                PackageStatus.Value = package.AssignmentTime is null ? BL.PackageStatus.created
                    : package.CollectionTime is null ? BL.PackageStatus.assigned
                    : package.DeliveryTime is null ? BL.PackageStatus.collected
                    : BL.PackageStatus.delivered;
            }
        }

        /// <summary>
        /// Called when the drone is updated. EventArgs is the ID of
        /// the drone. 
        /// </summary>
        public event EventHandler<int>? DroneUpdated;

        private void ChargeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = DroneId!.Value;
                Dm.SendDroneToCharge(Drone.Value!.Id);
                DroneUpdated?.Invoke(this, id);
            }
            catch (NoStationInRangeException)
            {
                MessageBox.Show("Drone is too far away from the nearest charging station.", "Out of Range",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReleaseButton_Click(object sender, RoutedEventArgs e)
        {
            var id = DroneId!.Value;
            Dm.ReleaseDroneFromCharge(Drone.Value!.Id);
            DroneUpdated?.Invoke(this, id);
        }

        private void UpdateDroneButton_Click(object sender, RoutedEventArgs e)
        {
            var id = DroneId!.Value;
            Dm.UpdateDrone(Drone.Value!.Id, NewModel.Value);
            DroneUpdated?.Invoke(this, id);
        }

        private void AssignPackageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = DroneId!.Value;
                Dm.AssignPackageToDrone(Drone.Value!.Id);
                DroneUpdated?.Invoke(this, id);
            }
            catch (NoRelevantPackageException)
            {
                MessageBox.Show(
                    "There are no packages for this drone to pick up.\nNote: There may be packages out of range and/or too heavy for this drone.",
                    "No Package to Pick Up", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CollectPackageButton_Click(object sender, RoutedEventArgs e)
        {
            var id = Drone.Value!.Id;
            Dm.CollectPackageByDrone(Drone.Value!.Id);
            DroneUpdated?.Invoke(this, id);
        }

        private void DeliverPackageButton_Click(object sender, RoutedEventArgs e)
        {
            var id = Drone.Value!.Id;
            Dm.DeliverPackageByDrone(Drone.Value!.Id);
            DroneUpdated?.Invoke(this, id);
        }

        private void ShowPackage(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow!).ShowPackage(Drone.Value!.Package!.Id);
        }

        private void ShowPackageSender(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow!).ShowCustomer(Drone.Value!.Package!.Sender.Id);
        }

        private void ShowPackageRecipient(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow!).ShowCustomer(Drone.Value!.Package!.Receiver.Id);
        }

        private void SimulatorCheckboxToggled(object sender, RoutedEventArgs e)
        {
            if (((CheckBox) sender).IsChecked!.Value)
                Dm.StartSimulator(DroneId!.Value);
            else Dm.StopSimulator(DroneId!.Value);
        }
    }
}