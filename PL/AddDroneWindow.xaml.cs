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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddDroneWindow.xaml
    /// </summary>
    public partial class AddDroneWindow : Window
    {
        static AddDroneWindow currentInstance = null;
        DroneListWindow itsWindow;

        internal static AddDroneWindow GetInstance(DroneListWindow itsWindow)
        {
            if (currentInstance == null)
            {
                currentInstance = new AddDroneWindow(itsWindow);
            }

            return currentInstance;
        }

        private AddDroneWindow(DroneListWindow itsWindow)
        {
            InitializeComponent();

            this.itsWindow = itsWindow;

            InitializeWeightSelector();
            InitializeStationSelector();
        }

        private void InitializeWeightSelector()
        {
            WeightCategoryMenu.Items.Add(new ComboBoxItem() { Content = "Heavy", Tag = (object)IDAL.DO.WeightCategory.heavy });
            WeightCategoryMenu.Items.Add(new ComboBoxItem() { Content = "Medium", Tag = (object)IDAL.DO.WeightCategory.medium });
            WeightCategoryMenu.Items.Add(new ComboBoxItem() { Content = "Light", Tag = (object)IDAL.DO.WeightCategory.light });
        }

        private void InitializeStationSelector()
        {
            foreach (IBL.BO.BaseStationListing station in DroneListWindow.bl.GetBaseStationList())
            {
                StationMenu.Items.Add(new ComboBoxItem() { Content = station.Name, Tag = (object)station });
            }
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            if (!int.TryParse(IDTextBox.Text, out id) || ModelTextBox.Text == "" || WeightCategoryMenu.SelectedItem is null || StationMenu.SelectedItem is null)
            {
                MessageBox.Show("One or more input is invalid.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                DroneListWindow.bl.AddDrone(
                    id,
                    ModelTextBox.Text,
                    (IDAL.DO.WeightCategory)((ComboBoxItem)WeightCategoryMenu.SelectedItem).Tag,
                    ((IBL.BO.BaseStationListing)((ComboBoxItem)StationMenu.SelectedItem).Tag).Id);

                itsWindow.DronesListView.ItemsSource = DroneListWindow.bl.GetDroneList();

                this.Close();
            }
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}