#nullable enable

using System;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateDronePage.xaml
    /// </summary>
    public partial class UpdateDronePage : Page
    {
        public DroneEditorDelegate Delegate;
        IBL.BO.DroneListing drone;
        public IBL.BO.DroneListing Drone
        {
            get => drone;
            set
            {
                drone = value;
                UpdateDroneInfo();
            }
        }

        public UpdateDronePage(DroneEditorDelegate editorDelegate, IBL.BO.DroneListing drone)
        {
            InitializeComponent();

            Delegate = editorDelegate;
            this.drone = drone;
            UpdateDroneInfo();
        }

        private void UpdateDroneInfo()
        {
            idLabel.Content = drone.Id;
            modelTextBox.Text = drone.Model;
            weightLabel.Content = drone.WeightCategory;
            batteryLabel.Content = string.Format("{0:0.00}%", drone.BatteryStatus);
            statusLabel.Content = drone.Status;
            locationLabel.Content = drone.Location;

            applyButton.IsEnabled = false;

            actionButtons.Visibility = Visibility.Visible;
            releaseConfirmStack.Visibility = Visibility.Hidden;

            chargeButton.Content = drone.Status == IBL.BO.DroneStatus.maintenance ? "Release" : "Charge";
            chargeButton.IsEnabled = drone.Status != IBL.BO.DroneStatus.delivering;


            if (drone.PackageId is null)
                packageButton.Content = "Assign Package";
            else
            {
                var package = Delegate.GetPackage(drone.PackageId.Value);
                packageButton.Content =
                    package.CollectionTime is null ? "Collect Package" : "Deliver Package";
            }

            packageButton.IsEnabled = drone.Status != IBL.BO.DroneStatus.maintenance;
        }

        private void ChargeButton_Click(object sender, RoutedEventArgs e)
        {
            if (drone.Status == IBL.BO.DroneStatus.maintenance)
            {
                chargeTimeBox.Clear();
                actionButtons.Visibility = Visibility.Hidden;
                releaseConfirmStack.Visibility = Visibility.Visible;
            }
            else
            {
                Delegate.SendDroneToCharge(drone.Id);
            }
        }

        private void UpdateDroneButton_Click(object sender, RoutedEventArgs e)
        {
            Delegate.UpdateDrone(drone.Id, modelTextBox.Text);
        }

        private void ModelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            applyButton.IsEnabled = modelTextBox.Text != drone.Model;
        }

        private void CancelRelease_Click(object sender, RoutedEventArgs e)
        {
            actionButtons.Visibility = Visibility.Visible;
            releaseConfirmStack.Visibility = Visibility.Hidden;
        }

        private void Release_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(chargeTimeBox.Text, out int chargingTime))
            {
                Delegate.ReleaseDroneFromCharge(drone.Id, chargingTime);
            }
            else
            {
                MessageBox.Show("The given charge time is invalid.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PackageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (drone.PackageId is null)
                {
                    Delegate.AssignPackageToDrone(drone.Id);
                }
                else
                {
                    var package = Delegate.GetPackage(drone.PackageId.Value);
                    if (package.CollectionTime is null)
                        Delegate.CollectPackageByDrone(drone.Id);
                    else
                        Delegate.DeliverPackageByDrone(drone.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateDroneInfo();
        }
    }
}