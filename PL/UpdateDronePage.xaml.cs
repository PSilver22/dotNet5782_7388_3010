#nullable enable

using System;
using System.Windows;
using System.Windows.Controls;
using PL.Utilities;
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateDronePage.xaml
    /// </summary>
    public partial class UpdateDronePage : Page
    {
        public IDroneEditor Delegate { get; set; }
        private BL.DroneListing drone;

        public BL.DroneListing Drone
        {
            get => drone;
            set
            {
                drone = value;
                UpdateDroneInfo();
            }
        }


        public DelegateCommand ReleaseDroneCommand { get; }
        public DelegateCommand UpdateDroneCommand { get; }

        /// <summary>
        /// Updates the displayed options on the page
        /// </summary>
        /// <param name="editorDelegate"></param>
        /// <param name="drone"></param>
        public UpdateDronePage(IDroneEditor editorDelegate, DroneListing drone)
        {
            InitializeComponent();

            Delegate = editorDelegate;
            this.drone = drone;
            UpdateDroneInfo();

            ReleaseDroneCommand = new DelegateCommand((_) => ReleaseDrone());
            UpdateDroneCommand = new DelegateCommand((_) => Delegate.UpdateDrone(drone.Id, modelTextBox.Text));
        }

        /// <summary>
        /// Updates drone info to content of inputs on page
        /// </summary>
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

            chargeButton.Content = drone.Status == BL.DroneStatus.maintenance ? "Release" : "Charge";
            chargeButton.IsEnabled = drone.Status != BL.DroneStatus.delivering;


            if (drone.PackageId is null)
                packageButton.Content = "Assign Package";
            else
            {
                var package = Delegate.GetPackage(drone.PackageId.Value);
                packageButton.Content =
                    package.CollectionTime is null ? "Collect Package" : "Deliver Package";
            }

            packageButton.IsEnabled = drone.Status != BL.DroneStatus.maintenance;
        }

        private void ChargeButton_Click(object sender, RoutedEventArgs e)
        {
            if (drone.Status == BL.DroneStatus.maintenance)
            {
                chargeTimeBox.Clear();
                actionButtons.Visibility = Visibility.Hidden;
                releaseConfirmStack.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    Delegate.SendDroneToCharge(drone.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
            ReleaseDrone();
        }

        /// <summary>
        /// Releases drone from charge
        /// </summary>
        private void ReleaseDrone()
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