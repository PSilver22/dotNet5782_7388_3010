using System.Windows;
using System.Windows.Input;
using IBL;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private IBL.IBL bl;

        public DroneListWindow()
        {
            InitializeComponent();
            bl = new BL();
            ReloadDrones();
        }

        public void ReloadDrones()
        {
            DronesListView.ItemsSource = bl.GetDroneList();
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
    }
}
