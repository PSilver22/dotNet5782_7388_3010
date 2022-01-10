using System.Windows;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IBL bl;

        public MainWindow()
        {
            InitializeComponent();
            bl = BLFactory.GetBl();
        }

        private void ShowDronesButton_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
        }
        private void ShowPackagesButton_Click(object sender, RoutedEventArgs e)
        {
            new PackagesListWindow(bl).ShowDialog();
        }
    }
}
