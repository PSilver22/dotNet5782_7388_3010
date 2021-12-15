using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using IBL;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private readonly IBL.IBL bl;

        public DroneListWindow(IBL.IBL bl)
        {
            InitializeComponent();
            
            this.bl = bl;
            
            FilterComboBox.ItemsSource = Enumerable.Prepend(
                ((IEnumerable<IBL.BO.DroneStatus>)Enum.GetValues(typeof(IBL.BO.DroneStatus)))
                .Select(s => new ComboBoxItem() { Content = s.ToString(), Tag = s }),
                new ComboBoxItem() { Content = "all drones", Tag = -1 });
            FilterComboBox.SelectedValue = -1;

            ReloadDrones();
        }

        public void ReloadDrones()
        {
            var drones = bl.GetDroneList();

            int tag = (int)(FilterComboBox.SelectedValue ?? -1);

            DronesListView.ItemsSource = tag == -1
                ? drones
                : drones.FindAll(d => d.Status == (IBL.BO.DroneStatus)tag);
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

        private void UpdateDroneDisplay()
        {
            if (DronesListView.SelectedItem is not null)
            {
                if (PageDisplay.Content is null)
                {
                    PageDisplay.Content = new UpdateDronePage(this, (IBL.BO.DroneListing)DronesListView.SelectedItem);
                }
                else
                {
                    ((UpdateDronePage)PageDisplay.Content).Drone = (IBL.BO.DroneListing)DronesListView.SelectedItem;
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
