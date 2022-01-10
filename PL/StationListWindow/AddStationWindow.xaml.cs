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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        public IStationAdder Delegate { get; set; }

        /// <summary>
        /// Constructor for the window
        /// </summary>
        /// <param name="adderDelegate">Object through which to add drones</param>
        public AddStationWindow(IStationAdder adderDelegate)
        {
            Delegate = adderDelegate;

            InitializeComponent();
        }

        /// <summary>
        /// Logic for click of add station button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStationButton_Click(object sender, RoutedEventArgs e) {
            string errorMessage = "ERROR: ";
            bool error = false;

            if (!int.TryParse(IDTextBox.Text, out int id)) {
                errorMessage += "Invalid ID\n";
                error = true;
            }

            if (NameTextBox.Text == "") {
                errorMessage += "Invalid Name\n";
                error = true;
            }

            if (!double.TryParse(LatitudeTextBox.Text, out double latitude)) {
                errorMessage += "Invalid Latitude\n";
                error = true;
            }

            if (!double.TryParse(LongitudeTextBox.Text, out double longitude)) {
                errorMessage += "Invalid Longitude\n";
                error = true;
            }

            if (!int.TryParse(ChargingSlotsTextBox.Text, out int chargeSlots)) {
                errorMessage += "Invalid # Charge Slots\n";
                error = true;
            }

            if (error) { MessageBox.Show(errorMessage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            else {
                Delegate.AddStation(
                    id,
                    NameTextBox.Text,
                    latitude,
                    longitude,
                    chargeSlots);

                Close();
            }
        }
    }
}
