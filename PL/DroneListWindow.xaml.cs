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
        static IBL.IBL bl = new BL();

        public DroneListWindow()
        {
            InitializeComponent();
            
            DronesListView.ItemsSource = bl.GetDroneList();
        }

        private void NewDrone_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void NewDrone_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
