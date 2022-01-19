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
using DO;
using PL.Utilities;

namespace PL
{
    public partial class PackageList : UserControl
    {
        public IBL Bl
        {
            get => (IBL) GetValue(BlProperty);
            set => SetValue(BlProperty, value);
        }

        public static readonly DependencyProperty BlProperty =
            DependencyProperty.Register("Bl", typeof(IBL), typeof(PackageList));

        public ObservableCollection<PackageListing> Packages { get; } = new();

        public Prop<int?> SelectedPackage { get; } = new();
        // public Prop<PackageListing?> SelectedPackageListing { get; } = new();
        // public Prop<BL.Package?> SelectedPackage { get; } = new();

        public WeightCategory? SelectedWeight { get; set; }
        public Priority? SelectedPriority { get; set; }
        public PackageStatus? SelectedStatus { get; set; }

        public enum Groups
        {
            sender,
            recipient
        }

        public Prop<Groups?> SelectedGrouping { get; set; } = new();

        private static readonly PropertyGroupDescription SenderGrouping = new("SenderName");
        private static readonly PropertyGroupDescription RecipientGrouping = new("ReceiverName");

        public PackageList()
        {
            Loaded += OnLoaded;

            var view = CollectionViewSource.GetDefaultView(Packages);
            view.Filter = ListFilter;
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            SelectedGrouping.PropertyChanged += OnSelectedGroupingChanged;

            InitializeComponent();
        }

        private void OnSelectedGroupingChanged(object? o, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(Packages);
            view.GroupDescriptions.Clear();
            switch (SelectedGrouping.Value)
            {
                case Groups.sender:
                    view.GroupDescriptions.Add(SenderGrouping);
                    break;
                case Groups.recipient:
                    view.GroupDescriptions.Add(RecipientGrouping);
                    break;
            }
        }

        private void OnLoaded(object o, RoutedEventArgs routedEventArgs)
        {
            // Refresh
            Packages.Clear();
            foreach (var p in Bl.GetPackageList()) Packages.Add(p);
            if (SelectedPackage.Value == null || Packages.All(p => p.Id != SelectedPackage.Value))
                SelectedPackage.Value = Packages.Any() ? Packages.First().Id : null;
            else SelectedPackage.Value = SelectedPackage.Value;
        }

        private bool ListFilter(object item)
        {
            var incl = true;
            if (SelectedWeight.HasValue)
                incl &= ((PackageListing) item).Weight == SelectedWeight.Value;
            if (SelectedPriority.HasValue)
                incl &= ((PackageListing) item).Priority == SelectedPriority.Value;
            if (SelectedStatus.HasValue)
                incl &= ((PackageListing) item).Status == SelectedStatus.Value;
            return incl;
        }

        private void NewPackage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var apw = new AddPackageWindow(Bl);
            apw.PackageAdded += id =>
            {
                var newPackage = Bl.GetPackageList().First(p => p.Id == id);
                Packages.Add(newPackage);
                SelectedPackage.Value = newPackage.Id;
            };
            apw.ShowDialog();
        }

        private void NewPackage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Packages).Refresh();
        }

        private void PackageUpdated(object? sender, int id)
        {
            Packages.Remove(Packages.Single(p => p.Id == id));
            var pkg = Bl.GetPackageList().First(p => p.Id == id);
            Packages.Add(pkg);
            SelectedPackage.Value = id;
        }
    }
}