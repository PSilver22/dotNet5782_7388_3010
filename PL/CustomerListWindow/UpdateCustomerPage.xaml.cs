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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Utilities;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateCustomerPage.xaml
    /// </summary>
    public partial class UpdateCustomerPage : Page
    {
        public ICustomerEditor Delegate { get; set; }
        private BL.Customer customer;

        public BL.Customer Customer {
            get => customer;
            set {
                customer = value;
                UpdateCustomerInfo();
            }
        }

        public DelegateCommand UpdateCustomerCommand { get; }

        public UpdateCustomerPage(ICustomerEditor editorDelegate, BL.Customer customer)
        {
            InitializeComponent();

            Delegate = editorDelegate;
            this.customer = customer;
            UpdateCustomerInfo();

            UpdateCustomerCommand = new DelegateCommand((_) => Delegate.UpdateCustomer(customer.Id, NameTextBox.Text, PhoneTextBox.Text));
        }

        private void UpdateCustomerInfo() {
            IdLabel.Content = customer.Id;
            NameTextBox.Text = customer.Name;
            PhoneTextBox.Text = customer.Phone;
            LocationLabel.Content = customer.Location.ToString();

            DeliveredPackagesListBox.ItemsSource = Delegate.GetDeliveredPackagesList(customer);
            SentPackagesListBox.ItemsSource = Delegate.GetSentPackagesList(customer);

            ApplyButton.IsEnabled = false;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            ApplyButton.IsEnabled = NameTextBox.Text != customer.Name && NameTextBox.Text != "";
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            ApplyButton.IsEnabled = PhoneTextBox.Text != customer.Phone && PhoneTextBox.Text != "";
        }

        private void PackageListBox_DoubleClick(object sender, MouseButtonEventArgs e) {
            // open package window, opened to selected drone
        }

        public void UpdateCustomerButton_Click(object sender, RoutedEventArgs e) {
            if (NameTextBox.Text == "") {
                MessageBox.Show("ERROR: Name is invalid.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (PhoneTextBox.Text == "") {
                MessageBox.Show("ERROR: Phone number is invalid.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                Delegate.UpdateCustomer(customer.Id, NameTextBox.Text, PhoneTextBox.Text);
            }
        }
    }
}
