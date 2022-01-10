#nullable enable
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddDroneWindow.xaml
    /// </summary>
    public partial class AddDroneWindow : Window
    {
        public IDroneAdder Delegate { get; set; }

        public AddDroneWindow(IDroneAdder adderDelegate)
        {
            InitializeComponent();

            Delegate = adderDelegate;
            InitializeWeightSelector();
            InitializeStationSelector();
            InitializeComponent();
        }

        private void InitializeWeightSelector()
        {
            WeightCategoryMenu.Items.Add(new ComboBoxItem() { Content = "Heavy", Tag = (object)DO.WeightCategory.heavy });
            WeightCategoryMenu.Items.Add(new ComboBoxItem() { Content = "Medium", Tag = (object)DO.WeightCategory.medium });
            WeightCategoryMenu.Items.Add(new ComboBoxItem() { Content = "Light", Tag = (object)DO.WeightCategory.light });
        }

        private void InitializeStationSelector()
        {
            foreach (BL.BaseStationListing station in Delegate.GetBaseStationList())
            {
                StationMenu.Items.Add(new ComboBoxItem() { Content = station.Name, Tag = (object)station });
            }
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IDTextBox.Text, out int id) || ModelTextBox.Text == "" || WeightCategoryMenu.SelectedItem is null || StationMenu.SelectedItem is null)
            {
                MessageBox.Show("One or more input is invalid.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                try
                {
                    Delegate.AddDrone(
                         id,
                         ModelTextBox.Text,
                         (DO.WeightCategory)((ComboBoxItem)WeightCategoryMenu.SelectedItem).Tag,
                         ((BL.BaseStationListing)((ComboBoxItem)StationMenu.SelectedItem).Tag).Id);

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}