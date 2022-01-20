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
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        private readonly BlApi.IBL bl;

        public CustomerListWindow(BlApi.IBL bl)
        {
            InitializeComponent();

            this.bl = bl;

            ReloadCustomers();
        }

        public void ReloadCustomers() {
            var customers = bl.GetCustomerList();

            CustomersListView.ItemsSource = customers;

            UpdateCustomerDisplay();
        }

        private void NewCustomer_Executed(object sender, ExecutedRoutedEventArgs e) {
            new AddCustomerWindow_Old(this).ShowDialog();
        }

        private void NewCustomer_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void CustomersListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateCustomerDisplay();
        }

        private void UpdateCustomerDisplay() {
            if (CustomersListView.SelectedItem is not null)
            {
                BL.Customer customer = bl.GetCustomer(((BL.CustomerListing)CustomersListView.SelectedItem).Id);
                if (PageDisplay.Content is null)
                {
                    PageDisplay.Content = new UpdateCustomerPage(this, customer);
                }
                else
                {
                    ((UpdateCustomerPage)PageDisplay.Content).Customer = customer;
                }
            }
            else {
                PageDisplay.Content = null;
            }
        }
    }
}
