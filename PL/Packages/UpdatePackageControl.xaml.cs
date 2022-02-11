#nullable enable

using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BL;
using BlApi;
using PL.Utilities;

namespace PL
{
    public partial class UpdatePackageControl : UserControl
    {
        public int? PackageId
        {
            get => (int?) GetValue(PackageIdProperty);
            set => SetValue(PackageIdProperty, value);
        }

        public static readonly DependencyProperty PackageIdProperty =
            DependencyProperty.Register("PackageId", typeof(int?), typeof(UpdatePackageControl),
                new PropertyMetadata(null, (sender, _) => (sender as UpdatePackageControl)?.UpdatePackage()));

        public Prop<Package?> Package { get; } = new();

        public IBL Bl
        {
            get => (IBL) GetValue(BlProperty);
            set => SetValue(BlProperty, value);
        }

        public static readonly DependencyProperty BlProperty =
            DependencyProperty.Register("Bl", typeof(IBL), typeof(UpdatePackageControl));

        public Prop<PackageStatus?> Status { get; } = new();

        public UpdatePackageControl()
        {
            InitializeComponent();
        }

        private void UpdatePackage()
        {
            Package.Value = PackageId is null ? null : Bl.GetPackage(PackageId.Value);
            Status.Value = Package.Value is null ? null
                : Package.Value.AssignmentTime is null ? PackageStatus.created
                : Package.Value.CollectionTime is null ? PackageStatus.assigned
                : Package.Value.DeliveryTime is null ? PackageStatus.collected
                : PackageStatus.delivered;
        }

        /// <summary>
        /// Called when the package is updated. EventArgs is the ID of
        /// the package. 
        /// </summary>
        public event EventHandler<int>? PackageUpdated;

        private void CollectPackage(object sender, RoutedEventArgs e)
        {
            var id = Package.Value!.Id;
            Bl.CollectPackageByDrone(Package.Value!.Drone!.Id);
            // Package.Value = Bl.GetPackage(Package.Value.Id);
            PackageUpdated?.Invoke(this, id);
        }

        private void DeliverPackage(object sender, RoutedEventArgs e)
        {
            var id = Package.Value!.Id;
            Bl.DeliverPackageByDrone(Package.Value!.Drone!.Id);
            // Package.Value = Bl.GetPackage(Package.Value.Id);
            PackageUpdated?.Invoke(this, id);
        }

        private void ShowDrone(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow!).ShowDrone(Package.Value!.Drone!.Id);
        }

        private void ShowSender(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow!).ShowCustomer(Package.Value!.Sender.Id);
        }
        
        private void ShowRecipient(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow!).ShowCustomer(Package.Value!.Receiver.Id);
        }
    }
}