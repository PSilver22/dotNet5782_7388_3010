using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private readonly BlApi.IBL bl;

        public DroneListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            
            this.bl = bl;
            
            StatusFilterComboBox.ItemsSource = Enumerable.Prepend(
                ((IEnumerable<BL.DroneStatus>)Enum.GetValues(typeof(BL.DroneStatus)))
                .Select(s => new ComboBoxItem() { Content = s.ToString(), Tag = s }),
                new ComboBoxItem() { Content = "all statuses", Tag = -1 });
            StatusFilterComboBox.SelectedValue = -1;
            
            WeightFilterComboBox.ItemsSource = Enumerable.Prepend(
                ((IEnumerable<DO.WeightCategory>)Enum.GetValues(typeof(DO.WeightCategory)))
                .Select(s => new ComboBoxItem() { Content = s.ToString(), Tag = s }),
                new ComboBoxItem() { Content = "all weights", Tag = -1 });
            WeightFilterComboBox.SelectedValue = -1;
            
            ReloadDrones();
        }

        /// <summary>
        /// Updates the drone list graphics to match the IBL drone list
        /// </summary>
        public void ReloadDrones()
        {
            var drones = bl.GetDroneList();

            int statusTag = (int)(StatusFilterComboBox.SelectedValue ?? -1);

            drones = statusTag == -1
                ? drones
                : drones.FindAll(d => d.Status == (BL.DroneStatus)statusTag);

            int weightTag = (int)(WeightFilterComboBox.SelectedValue ?? -1);

            drones = weightTag == -1
                ? drones
                : drones.FindAll(d => d.WeightCategory == (DO.WeightCategory)weightTag);

            DronesListView.ItemsSource = drones;

            UpdateDroneDisplay();
        }

        private void NewDrone_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            new AddDroneWindow(this).ShowDialog();
        }

        private void NewDrone_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DronesListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateDroneDisplay();
        }

        /// <summary>
        /// Updates the drone display to show the UpdateDronePage for the current drone
        /// </summary>
        private void UpdateDroneDisplay()
        {
            if (DronesListView.SelectedItem is not null)
            {
                if (PageDisplay.Content is null)
                {
                    PageDisplay.Content = new UpdateDronePage(this, (BL.DroneListing)DronesListView.SelectedItem);
                }
                else
                {
                    ((UpdateDronePage)PageDisplay.Content).Drone = (BL.DroneListing)DronesListView.SelectedItem;
                }
            }
            else
            {
                PageDisplay.Content = null;
            }
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadDrones();
        }
    }
}
