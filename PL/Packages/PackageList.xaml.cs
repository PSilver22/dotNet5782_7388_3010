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
        public DataManager Dm
        {
            get => (DataManager) GetValue(DmProperty);
            set => SetValue(DmProperty, value);
        }

        public static readonly DependencyProperty DmProperty =
            DependencyProperty.Register(nameof(Dm), typeof(DataManager), typeof(PackageList));

        public Prop<int?> SelectedPackage { get; } = new();
        private int? _lastSelectedPackage;

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
        }

        private void OnSelectedGroupingChanged(object? o, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(Dm.Packages);
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
            InitializeComponent();
            
            SelectedPackage.PropertyChanged += (_, _) =>
            {
                if (SelectedPackage.Value.HasValue)
                    _lastSelectedPackage = SelectedPackage.Value;
            };
            Dm.Packages.CollectionChanged += (_, _) =>
            {
                if (!SelectedPackage.Value.HasValue && _lastSelectedPackage.HasValue)
                    SelectedPackage.Value = _lastSelectedPackage;
            };

            var view = CollectionViewSource.GetDefaultView(Dm.Packages);
            view.Filter = ListFilter;
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            SelectedGrouping.PropertyChanged += OnSelectedGroupingChanged;

            Loaded -= OnLoaded;
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
            var apw = new AddPackageWindow(Dm);
            apw.PackageAdded += id => SelectedPackage.Value = id;
            apw.ShowDialog();
        }

        private void NewPackage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Dm.Packages).Refresh();
        }

        private void PackageUpdated(object? sender, int id)
        {
            SelectedPackage.Value = id;
        }
    }
}