using System;
using System.Windows;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBL Bl { get; set; }

        public MainWindow()
        {
            Bl = BLFactory.GetBl();
            InitializeComponent();
        }

        public void ShowDrone(int id)
        {
            TabControl.SelectedIndex = 0;
            DroneList.SelectedDrone.Value = id;
        }
        
        public void ShowPackage(int id)
        {
            TabControl.SelectedIndex = 1;
            PackageList.SelectedPackage.Value = id;
        }

        public void ShowCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
