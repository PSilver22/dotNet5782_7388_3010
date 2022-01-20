#nullable enable

using System;
using System.Windows;
using System.Windows.Input;
using BlApi;
using MapControl;
using PL.Utilities;

namespace PL
{
    public partial class AddCustomerWindow : Window
    {
        private IBL bl;

        public Prop<int?> Id { get; set; } = new();
        public Prop<string> CustomerName { get; set; } = new();
        public Prop<string> Phone { get; set; } = new();

        public Prop<Location> Loc { get; } = new()
        {
            Value = new Location(31.765, 35.1906)
        };

        /// <summary>
        /// Called when a customer is added
        /// </summary>
        public event Action<int>? CustomerAdded;

        public AddCustomerWindow(IBL bl)
        {
            this.bl = bl;

            InitializeComponent();
        }

        private void MapClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Loc.Value = Map.ViewToLocation(e.GetPosition(Map));
            }
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddCustomer(Id.Value!.Value, CustomerName.Value, Phone.Value, Loc.Value.Longitude,
                    Loc.Value.Latitude);
                Close();
                CustomerAdded?.Invoke(Id.Value!.Value);
            }
            catch (DO.DuplicatedIdException)
            {
                MessageBox.Show("The selected ID is already in use.",
                    "Duplicated ID",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}