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
        public DataManager Dm
        {
            get => (DataManager) GetValue(DmProperty);
            set => SetValue(DmProperty, value);
        }

        public static readonly DependencyProperty DmProperty =
            DependencyProperty.Register(nameof(Dm), typeof(DataManager), typeof(DroneList));

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
        }

        private void OnSelectedGroupingOnPropertyChanged(object? o, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var view = CollectionViewSource.GetDefaultView(Dm.Drones);
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
            InitializeComponent();
            
            var view = CollectionViewSource.GetDefaultView(Dm.Drones);
            view.Filter = ListFilter;
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            SelectedGrouping.PropertyChanged += OnSelectedGroupingOnPropertyChanged;

            Loaded -= OnLoaded;
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
            var adw = new AddDroneWindow(Dm);
            adw.DroneAdded += id => SelectedDrone.Value = id;
            adw.ShowDialog();
        }

        private void NewDrone_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Dm.Drones).Refresh();
        }

        private void DroneUpdated(object? sender, int id)
        {
            SelectedDrone.Value = id;
        }
    }
}