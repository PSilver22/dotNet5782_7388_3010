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
    public partial class DroneList : UserControl
    {
        public IBL Bl
        {
            get => (IBL) GetValue(BlProperty);
            set => SetValue(BlProperty, value);
        }

        public static readonly DependencyProperty BlProperty =
            DependencyProperty.Register(nameof(Bl), typeof(IBL), typeof(DroneList));

        public ObservableCollection<DroneListing> Drones { get; } = new();
        public Prop<int?> SelectedDrone { get; } = new();

        public WeightCategory? SelectedWeight { get; set; }
        public DroneStatus? SelectedStatus { get; set; }

        public enum Groups
        {
            status,
            [Description("weight class")]
            weightClass
        }

        public Prop<Groups?> SelectedGrouping { get; set; } = new();

        private static readonly PropertyGroupDescription StatusGrouping = new("Status");
        private static readonly PropertyGroupDescription WeightGrouping = new("WeightCategory");
        
        public DroneList()
        {
            Loaded += OnLoaded;
            
            var view = CollectionViewSource.GetDefaultView(Drones);
            view.Filter = ListFilter;
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            SelectedGrouping.PropertyChanged += OnSelectedGroupingOnPropertyChanged;
            
            InitializeComponent();
        }

        private void OnSelectedGroupingOnPropertyChanged(object? o, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(Drones);
            view.GroupDescriptions.Clear();
            switch (SelectedGrouping.Value)
            {
                case Groups.status:
                    view.GroupDescriptions.Add(StatusGrouping);
                    break;
                case Groups.weightClass:
                    view.GroupDescriptions.Add(WeightGrouping);
                    break;
            }
        }

        private void OnLoaded(object o, RoutedEventArgs routedEventArgs)
        {
            // Refresh
            var droneId = SelectedDrone.Value;
            Drones.Clear();
            foreach (var d in Bl.GetDroneList()) Drones.Add(d);
            if (droneId == null || Drones.All(d => d.Id != droneId))
                SelectedDrone.Value = Drones.Any() ? Drones.First().Id : null;
            else SelectedDrone.Value = droneId;
            List.ScrollIntoView(List.SelectedItem);
        }
        
        private bool ListFilter(object item)
        {
            var incl = true;
            if (SelectedWeight.HasValue)
                incl &= (item as DroneListing)!.WeightCategory == SelectedWeight.Value;
            if (SelectedStatus.HasValue)
                incl &= (item as DroneListing)!.Status == SelectedStatus.Value;
            return incl;
        }

        private void NewDrone_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var adw = new AddDroneWindow(Bl);
            adw.DroneAdded += id =>
            {
                var newDrone = Bl.GetDroneList().First(d => d.Id == id);
                Drones.Add(newDrone);
                SelectedDrone.Value = id;
            };
            adw.ShowDialog();
        }

        private void NewDrone_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Drones).Refresh();
        }

        private void DroneUpdated(object? sender, int id)
        {
            Drones.Remove(Drones.Single(d => d.Id == id));
            var drone = Bl.GetDroneList().First(d => d.Id == id);
            Drones.Add(drone);
            SelectedDrone.Value = id;
        }
    }
}