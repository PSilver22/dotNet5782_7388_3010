#nullable enable

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BlApi;
using DO;
using PL.Utilities;

namespace PL.PackageList
{
    public partial class AddPackageWindow : Window
    {
        private IBL bl;

        public ObservableCollection<ComboBoxItem> Customers { get; set; }

        public Prop<int?> SenderId { get; set; } = new();
        public Prop<int?> RecipientId { get; set; } = new();
        public Prop<WeightCategory?> Weight { get; set; } = new();
        public Prop<Priority?> Priority { get; set; } = new();

        /// <summary>
        /// Called when a package is added
        /// </summary>
        public event Action<int>? PackageAdded;

        public AddPackageWindow(IBL bl)
        {
            this.bl = bl;

            Customers = new ObservableCollection<ComboBoxItem>(
                bl.GetCustomerList()
                    .Select(c => new ComboBoxItem{Content = $"[{c.Id}] {c.Name}", Tag = c.Id}));
            
            InitializeComponent();
        }

        private void AddPackageButton_Click(object sender, RoutedEventArgs e)
        {
            var newId = bl.AddPackage(SenderId.Value!.Value, RecipientId.Value!.Value,
                Weight.Value!.Value, Priority.Value!.Value);
            Close();
            PackageAdded?.Invoke(newId);
        }
    }
}