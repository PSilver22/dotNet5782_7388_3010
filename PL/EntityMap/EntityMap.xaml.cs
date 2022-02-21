#nullable enable
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        public DataManager Dm
        {
            get => (DataManager) GetValue(DmProperty);
            set => SetValue(DmProperty, value);
        }

        public static readonly DependencyProperty DmProperty =
            DependencyProperty.Register(nameof(Dm), typeof(DataManager), typeof(EntityMap));

        public Prop<BL.Location> Center { get; } = new()
        {
            Value = new Location(31.765, 35.1906)
        };

        public EntityMap()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object o, RoutedEventArgs routedEventArgs)
        {
            InitializeComponent();
            // Loaded -= OnLoaded;
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