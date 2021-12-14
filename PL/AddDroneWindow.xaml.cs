#nullable enable
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddDroneWindow.xaml
    /// </summary>
    public partial class AddDroneWindow : Window
    {
        public DroneAdderDelegate Delegate { get; set; }

        public AddDroneWindow(DroneAdderDelegate adderDelegate)
        {
            InitializeComponent();

            Delegate = adderDelegate;
            InitializeWeightSelector();
            InitializeStationSelector();
            InitializeComponent();
        }

        private void InitializeWeightSelector()
        {
            WeightCategoryMenu.Items.Add(new ComboBoxItem() { Content = "Heavy", Tag = (object)IDAL.DO.WeightCategory.heavy });
            WeightCategoryMenu.Items.Add(new ComboBoxItem() { Content = "Medium", Tag = (object)IDAL.DO.WeightCategory.medium });
            WeightCategoryMenu.Items.Add(new ComboBoxItem() { Content = "Light", Tag = (object)IDAL.DO.WeightCategory.light });
        }

        private void InitializeStationSelector()
        {
            foreach (IBL.BO.BaseStationListing station in Delegate.GetBaseStationList())
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
               Delegate.AddDrone(
                    id,
                    ModelTextBox.Text,
                    (IDAL.DO.WeightCategory)((ComboBoxItem)WeightCategoryMenu.SelectedItem).Tag,
                    ((IBL.BO.BaseStationListing)((ComboBoxItem)StationMenu.SelectedItem).Tag).Id);

                Close();
            }
        }
    }
}