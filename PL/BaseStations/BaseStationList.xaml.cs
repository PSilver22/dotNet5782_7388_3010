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
    public partial class BaseStationList : UserControl
    {
        public DataManager Dm
        {
            get => (DataManager) GetValue(DmProperty);
            set => SetValue(DmProperty, value);
        }

        public static readonly DependencyProperty DmProperty =
            DependencyProperty.Register(nameof(Dm), typeof(DataManager), typeof(BaseStationList));

        public Prop<int?> SelectedStation { get; } = new();
        private int? _lastSelectedStation = null;

        public enum Groups
        {
            [Description("Availability")] availability,
            [Description("# of Available")] availableCount
        }

        public Prop<Groups?> SelectedGrouping { get; set; } = new();

        private static readonly PropertyGroupDescription AvailabilityGrouping = new("AvailableChargingSlotsCount",
            new LambdaConverter<string>(count => (int) count! > 0 ? "Has Available Slots" : "No Available Slots"));

        private static readonly PropertyGroupDescription NumAvailableGrouping = new("AvailableChargingSlotsCount");

        public BaseStationList()
        {
            Loaded += OnLoaded;
            Loaded += (_, _) => List.Focus();
        }

        private void OnSelectedGroupingChanged(object? o, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(Dm.Stations);
            view.GroupDescriptions.Clear();
            switch (SelectedGrouping.Value)
            {
                case Groups.availability:
                    view.GroupDescriptions.Add(AvailabilityGrouping);
                    break;
                case Groups.availableCount:
                    view.GroupDescriptions.Add(NumAvailableGrouping);
                    break;
            }
        }

        private void OnLoaded(object o, RoutedEventArgs routedEventArgs)
        {
            InitializeComponent();

            SelectedStation.PropertyChanged += (_, _) =>
            {
                if (SelectedStation.Value.HasValue)
                    _lastSelectedStation = SelectedStation.Value;
                else if (_lastSelectedStation.HasValue) SelectedStation.Value = _lastSelectedStation;
            };
            Dm.Stations.CollectionChanged += (_, _) => { SelectedStation.Value = _lastSelectedStation; };

            var view = CollectionViewSource.GetDefaultView(Dm.Stations);
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            SelectedGrouping.PropertyChanged += OnSelectedGroupingChanged;

            Loaded -= OnLoaded;
        }

        private void NewStation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var asw = new AddBaseStationWindow(Dm);
            asw.StationAdded += id => { SelectedStation.Value = id; };
            asw.ShowDialog();
        }

        private void NewStation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void StationUpdated(object? sender, int id)
        {
            SelectedStation.Value = id;
        }
    }
}