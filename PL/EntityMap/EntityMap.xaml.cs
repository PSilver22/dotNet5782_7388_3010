#nullable enable
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BL;
using BlApi;
using MapControl;
using PL.Utilities;
using Location = BL.Location;

namespace PL
{
    public partial class EntityMap : UserControl
    {
        public IBL Bl
        {
            get => (IBL) GetValue(BlProperty);
            set => SetValue(BlProperty, value);
        }

        public static readonly DependencyProperty BlProperty =
            DependencyProperty.Register(nameof(Bl), typeof(IBL), typeof(EntityMap));

        public ObservableCollection<BaseStation> Stations { get; } = new();
        public ObservableCollection<Drone> Drones { get; } = new();
        public ObservableCollection<Customer> Customers { get; } = new();

        public Prop<BL.Location> Center { get; } = new()
        {
            Value = new Location(31.765, 35.1906)
        };

        public EntityMap()
        {
            Loaded += OnLoaded;

            InitializeComponent();
        }

        private void OnLoaded(object o, RoutedEventArgs routedEventArgs)
        {
            // Refresh
            Stations.Clear();
            Drones.Clear();
            Customers.Clear();
            
            foreach (var s in Bl.GetBaseStationList())
                Stations.Add(Bl.GetBaseStation(s.Id));
            
            foreach (var d in Bl.GetDroneList())
                Drones.Add(Bl.GetDrone(d.Id));
            
            foreach (var c in Bl.GetCustomerList())
                Customers.Add(Bl.GetCustomer(c.Id));
        }

        private void ShowStation(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2) return;
            ((MainWindow)Application.Current.MainWindow!).ShowStation((int)((MapItem)sender).Tag);
            e.Handled = true;
        }
        
        private void ShowDrone(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2) return;
            ((MainWindow)Application.Current.MainWindow!).ShowDrone((int)((MapItem)sender).Tag);
            e.Handled = true;
        }
        
        private void ShowCustomer(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2) return;
            ((MainWindow)Application.Current.MainWindow!).ShowCustomer((int)((MapItem)sender).Tag);
            e.Handled = true;
        }
    }
}