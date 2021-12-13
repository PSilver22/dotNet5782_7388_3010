using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateDronePage.xaml
    /// </summary>
    public partial class UpdateDronePage : Page
    {
        DroneListWindow itsWindow;
        IBL.BO.DroneListing droneListing;

        public UpdateDronePage(DroneListWindow itsWindow, IBL.BO.DroneListing drone)
        {
            InitializeComponent();

            this.droneListing = drone;
            this.itsWindow = itsWindow;

            DroneTextBlock.Text = droneListing.ToString();

            UpdateChangeDroneStateButton();
        }

        private void UpdateDroneInfo() {
            DroneTextBlock.Text = droneListing.ToString();
        }

        private void Unplug_Click(object sender, RoutedEventArgs e)
        {
            int chargingTime;

            if (int.TryParse(ChargeTimeTextBox.Text, out chargingTime))
            {
                DroneListWindow.bl.ReleaseDroneFromCharge(droneListing.Id, chargingTime);

                ChargeTimeLabel.Visibility = Visibility.Hidden;
                ChargeTimeTextBox.Visibility = Visibility.Hidden;
                ChargeTimeTextBox.Text = "";

                itsWindow.DronesListView.ItemsSource = DroneListWindow.bl.GetDroneList();

                UpdateDroneInfo();
                UpdateChangeDroneStateButton();
            }

            else
            {
                MessageBox.Show("The given charge time is invalid.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PlugIn_Click(object sender, RoutedEventArgs e)
        {
            DroneListWindow.bl.SendDroneToCharge(droneListing.Id);

            PlugInDroneButton.Visibility = Visibility.Hidden;

            itsWindow.DronesListView.ItemsSource = DroneListWindow.bl.GetDroneList();

            UpdateDroneInfo();
            UpdateChangeDroneStateButton();
        }

        private void AssignDronePackage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DroneListWindow.bl.AssignPackageToDrone(droneListing.Id);

                itsWindow.DronesListView.ItemsSource = DroneListWindow.bl.GetDroneList();

                UpdateDroneInfo();
                UpdateChangeDroneStateButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CollectDronePackage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DroneListWindow.bl.CollectPackageByDrone(droneListing.Id);

                itsWindow.DronesListView.ItemsSource = DroneListWindow.bl.GetDroneList();

                UpdateDroneInfo();
                UpdateChangeDroneStateButton();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeliverDronePackage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DroneListWindow.bl.DeliverPackageByDrone(droneListing.Id);

                itsWindow.DronesListView.ItemsSource = DroneListWindow.bl.GetDroneList();

                UpdateDroneInfo();
                UpdateChangeDroneStateButton();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private RoutedEventHandler lastAction = null;
        private void UpdateChangeDroneStateButton() 
        {
            if (lastAction is not null)
            {
                ChangeDroneStateButton.Click -= lastAction;
            }

            if (droneListing.Status == IBL.BO.DroneStatus.maintenance)
            {
                lastAction = new RoutedEventHandler(Unplug_Click);
                ChangeDroneStateButton.Click += lastAction;

                ChargeTimeLabel.Visibility = Visibility.Visible;
                ChargeTimeTextBox.Visibility = Visibility.Visible;

                ChangeDroneStateButton.Content = "Unplug Drone";
            }

            else if (droneListing.Status == IBL.BO.DroneStatus.free)
            {
                PlugInDroneButton.Visibility = Visibility.Visible;

                lastAction = new RoutedEventHandler(AssignDronePackage_Click);
                ChangeDroneStateButton.Click += lastAction;

                ChangeDroneStateButton.Content = "Assign Package";
            }

            else
            {
                IBL.BO.Package package = DroneListWindow.bl.GetPackage(droneListing.PackageId ?? -1);

                if (package.CollectionTime is null)
                {
                    lastAction = new RoutedEventHandler(CollectDronePackage_Click);
                    ChangeDroneStateButton.Click += lastAction;

                    ChangeDroneStateButton.Content = "Collect Package";
                }

                else
                {
                    lastAction = new RoutedEventHandler(DeliverDronePackage_Click);
                    ChangeDroneStateButton.Click += new RoutedEventHandler(DeliverDronePackage_Click);

                    ChangeDroneStateButton.Content = "Deliver Package";
                }
            }
        }

        private void UpdateDroneButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewModelTextBox.Text != "") {
                droneListing.Model = NewModelTextBox.Text;

                NewModelTextBox.Text = "";

                UpdateDroneInfo();
            }
        }
    }
}