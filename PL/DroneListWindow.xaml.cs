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
        internal static IBL.IBL bl = new BL();

        public DroneListWindow()
        {
            InitializeComponent();
            
            DronesListView.ItemsSource = bl.GetDroneList();
        }

        private void NewDrone_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddDroneWindow.GetInstance(this).ShowDialog();
        }

        private void NewDrone_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DronesListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DronesListView.SelectedItems is not null)
            {
                PageDisplay.Content = new UpdateDronePage(this, (IBL.BO.DroneListing)DronesListView.SelectedItem);
            }

            else
            {
                PageDisplay.Content = null;
            }
        }
    }
}
