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
        public DataManager Dm
        {
            get => (DataManager) GetValue(DmProperty);
            set => SetValue(DmProperty, value);
        }

        public static readonly DependencyProperty DmProperty =
            DependencyProperty.Register(nameof(Dm), typeof(DataManager), typeof(CustomerList));
        
        public Prop<int?> SelectedCustomer { get; } = new();
        
        public CustomerList()
        {
            Loaded += OnLoaded;
            
            InitializeComponent();
        }
        

        private void OnLoaded(object o, RoutedEventArgs routedEventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(Dm.Customers);
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            Loaded -= OnLoaded;
        }

        private void NewCustomer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var acw = new AddCustomerWindow(Dm);
            acw.CustomerAdded += id =>
            {
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
            SelectedCustomer.Value = id;
        }
    }
}