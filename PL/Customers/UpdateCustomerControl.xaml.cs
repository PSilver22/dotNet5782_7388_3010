#nullable enable

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BL;
using BlApi;
using PL.Utilities;

namespace PL
{
    public partial class UpdateCustomerControl : UserControl
    {
        public int? CustomerId
        {
            get => (int?) GetValue(CustomerIdProperty);
            set => SetValue(CustomerIdProperty, value);
        }

        public static readonly DependencyProperty CustomerIdProperty =
            DependencyProperty.Register(nameof(CustomerId), typeof(int?), typeof(UpdateCustomerControl),
                new PropertyMetadata(null, (sender, _) => (sender as UpdateCustomerControl)?.LoadCustomer()));

        public Prop<Customer?> Customer { get; } = new();
        
        public IBL Bl
        {
            get => (IBL) GetValue(BlProperty);
            set => SetValue(BlProperty, value);
        }

        public static readonly DependencyProperty BlProperty =
            DependencyProperty.Register(nameof(Bl), typeof(IBL), typeof(UpdateCustomerControl));
        
        public DelegateCommand UpdateCustomerCommand { get; }
        
        public Prop<string> NewName { get; } = new();
        public Prop<string> NewPhone { get; } = new();
        
        public UpdateCustomerControl()
        {
            UpdateCustomerCommand = new DelegateCommand((_) =>
            {
                if (Customer.Value?.Name != NewName.Value || Customer.Value.Phone != NewPhone.Value)
                {
                    var id = CustomerId!.Value;
                    Bl.UpdateCustomer(CustomerId!.Value, NewName.Value, NewPhone.Value);
                    CustomerUpdated?.Invoke(this, id);
                }
            });
            
            InitializeComponent();
        }
        
        private void LoadCustomer()
        {
            Customer.Value = CustomerId is null ? null : Bl.GetCustomer(CustomerId.Value);

            NewName.Value = Customer.Value?.Name ?? string.Empty;
            NewPhone.Value = Customer.Value?.Phone ?? string.Empty;
        }

        /// <summary>
        /// Called when the customer is updated. EventArgs is the ID of
        /// the customer. 
        /// </summary>
        public event EventHandler<int>? CustomerUpdated;
        
        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var id = Customer.Value!.Id;
            Bl.UpdateCustomer(Customer.Value!.Id, NewName.Value, NewPhone.Value);
            CustomerUpdated?.Invoke(this, id);
        }

        private void ShowPackage(object sender, MouseButtonEventArgs e)
        {
            var id = (int)((ListView) sender).SelectedValue;
            ((MainWindow)Application.Current.MainWindow!).ShowPackage(id);
        }
    }
}