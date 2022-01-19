#nullable enable

using System;
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

        public IBL Bl
        {
            get => (IBL) GetValue(BlProperty);
            set => SetValue(BlProperty, value);
        }

        public static readonly DependencyProperty BlProperty =
            DependencyProperty.Register("Bl", typeof(IBL), typeof(UpdateDroneControl));


        public Prop<string> NewModel { get; } = new();
        public DelegateCommand UpdateDroneCommand { get; }

        /// <summary>
        /// Display and interaction control for drones
        /// </summary>
        public UpdateDroneControl()
        {
            UpdateDroneCommand = new DelegateCommand((_) =>
            {
                if (Drone.Value?.Model != NewModel.Value)
                {
                    Bl.UpdateDrone(DroneId!.Value, NewModel.Value);
                    DroneUpdated?.Invoke(this, Drone.Value!.Id);
                }
            });
            
            InitializeComponent();
        }

        private void LoadDrone()
        {
            Drone.Value = DroneId is null ? null : Bl.GetDrone(DroneId.Value);

            NewModel.Value = Drone.Value?.Model ?? string.Empty;

            if (Drone.Value?.Package is null)
            {
                PackageStatus.Value = null;
            }
            else
            {
                var package = Bl.GetPackage(Drone.Value.Package.Id);
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
                Bl.SendDroneToCharge(Drone.Value!.Id);
                DroneUpdated?.Invoke(this, Drone.Value!.Id);
            }
            catch (NoStationInRangeException)
            {
                MessageBox.Show("Drone is too far away from the nearest charging station.", "Out of Range",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReleaseButton_Click(object sender, RoutedEventArgs e)
        {
            Bl.ReleaseDroneFromCharge(Drone.Value!.Id);
            DroneUpdated?.Invoke(this, Drone.Value.Id);
        }

        private void UpdateDroneButton_Click(object sender, RoutedEventArgs e)
        {
            Bl.UpdateDrone(Drone.Value!.Id, NewModel.Value);
            DroneUpdated?.Invoke(this, Drone.Value.Id);
        }

        private void AssignPackageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.AssignPackageToDrone(Drone.Value!.Id);
                DroneUpdated?.Invoke(this, Drone.Value.Id);
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
            Bl.CollectPackageByDrone(Drone.Value!.Id);
            DroneUpdated?.Invoke(this, Drone.Value!.Id);
        }

        private void DeliverPackageButton_Click(object sender, RoutedEventArgs e)
        {
            Bl.DeliverPackageByDrone(Drone.Value!.Id);
            DroneUpdated?.Invoke(this, Drone.Value.Id);
        }

        private void ShowPackage(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow!).ShowPackage(Drone.Value!.Package!.Id);
        }

        private void ShowPackageSender(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow!).ShowCustomer(Drone.Value!.Package!.Sender.Id);
        }

        private void ShowPackageRecipient(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow!).ShowCustomer(Drone.Value!.Package!.Receiver.Id);
        }
    }
}