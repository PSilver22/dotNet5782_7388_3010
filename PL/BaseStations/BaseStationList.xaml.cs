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
        public IBL Bl
        {
            get => (IBL) GetValue(BlProperty);
            set => SetValue(BlProperty, value);
        }

        public static readonly DependencyProperty BlProperty =
            DependencyProperty.Register(nameof(Bl), typeof(IBL), typeof(BaseStationList));

        public ObservableCollection<BaseStationListing> Stations { get; } = new();

        public Prop<int?> SelectedStation { get; } = new();

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

            var view = CollectionViewSource.GetDefaultView(Stations);
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            
            SelectedGrouping.PropertyChanged += OnSelectedGroupingChanged;

            InitializeComponent();
        }
        
        private void OnSelectedGroupingChanged(object? o, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(Stations);
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
            // Refresh
            var stationId = SelectedStation.Value;
            Stations.Clear();
            foreach (var s in Bl.GetBaseStationList()) Stations.Add(s);
            if (stationId is null || Stations.All(s => s.Id != stationId))
                SelectedStation.Value = Stations.Any() ? Stations.First().Id : null;
            else SelectedStation.Value = stationId;
            List.ScrollIntoView(List.SelectedItem);
        }

        private void NewStation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var asw = new AddBaseStationWindow(Bl);
            asw.StationAdded += id =>
            {
                var newStation = Bl.GetBaseStationList().First(s => s.Id == id);
                Stations.Add(newStation);
                SelectedStation.Value = newStation.Id;
            };
            asw.ShowDialog();
        }

        private void NewStation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void StationUpdated(object? sender, int id)
        {
            Stations.Remove(Stations.Single(s => s.Id == id));
            var station = Bl.GetBaseStationList().First(d => d.Id == id);
            Stations.Add(station);
            SelectedStation.Value = id;
        }
    }
}