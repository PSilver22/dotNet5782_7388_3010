#nullable enable

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using BL;
using BlApi;
using PL.Utilities;

namespace PL
{
    public partial class CustomerList : UserControl
    {
        public IBL Bl
        {
            get => (IBL) GetValue(BlProperty);
            set => SetValue(BlProperty, value);
        }

        public static readonly DependencyProperty BlProperty =
            DependencyProperty.Register(nameof(Bl), typeof(IBL), typeof(CustomerList));

        public ObservableCollection<CustomerListing> Customers { get; } = new();
        public Prop<int?> SelectedCustomer { get; } = new();
        
        public CustomerList()
        {
            Loaded += OnLoaded;
            
            var view = CollectionViewSource.GetDefaultView(Customers);
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            InitializeComponent();
        }
        

        private void OnLoaded(object o, RoutedEventArgs routedEventArgs)
        {
            // Refresh
            var customerId = SelectedCustomer.Value;
            Customers.Clear();
            foreach (var c in Bl.GetCustomerList()) Customers.Add(c);
            if (customerId == null || Customers.All(c => c.Id != customerId))
                SelectedCustomer.Value = Customers.Any() ? Customers.First().Id : null;
            else SelectedCustomer.Value = customerId;
            List.ScrollIntoView(List.SelectedItem);
        }

        private void NewCustomer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var acw = new AddCustomerWindow(Bl);
            acw.CustomerAdded += id =>
            {
                var newCustomer = Bl.GetCustomerList().First(c => c.Id == id);
                Customers.Add(newCustomer);
                SelectedCustomer.Value = id;
            };
            acw.ShowDialog();
        }

        private void NewCustomer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CustomerUpdated(object? sender, int id)
        {
            Customers.Remove(Customers.Single(c => c.Id == id));
            var customer = Bl.GetCustomerList().First(c => c.Id == id);
            Customers.Add(customer);
            SelectedCustomer.Value = id;
        }
    }
}