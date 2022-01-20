#nullable enable
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BL;
using BlApi;
using PL.Utilities;

namespace PL
{
    public partial class UpdateBaseStationControl : UserControl
    {
        public int? StationId
        {
            get => (int?) GetValue(StationIdProperty);
            set => SetValue(StationIdProperty, value);
        }

        public static readonly DependencyProperty StationIdProperty =
            DependencyProperty.Register(nameof(StationId), typeof(int?), typeof(UpdateBaseStationControl),
                new PropertyMetadata(null, (sender, _) => (sender as UpdateBaseStationControl)?.LoadStation()));

        public Prop<BaseStation?> Station { get; } = new();
        
        public IBL Bl
        {
            get => (IBL) GetValue(BlProperty);
            set => SetValue(BlProperty, value);
        }

        public static readonly DependencyProperty BlProperty =
            DependencyProperty.Register(nameof(Bl), typeof(IBL), typeof(UpdateBaseStationControl));
        
        public DelegateCommand UpdateStationCommand { get; }
        
        public Prop<string> NewName { get; } = new();
        public Prop<ushort> NewNumChargingSlots { get; } = new();
        
        public UpdateBaseStationControl()
        {
            UpdateStationCommand = new DelegateCommand((_) =>
            {
                if (Station.Value?.Name != NewName.Value || Station.Value.AvailableChargingSlots != NewNumChargingSlots.Value)
                {
                    if (!Validation.GetHasError((TextBox)ChargingSlotsItem.Value))
                    {
                        Bl.UpdateBaseStation(StationId!.Value, NewName.Value, NewNumChargingSlots.Value);
                        StationUpdated?.Invoke(this, Station.Value!.Id);
                    }
                }
            });
            
            InitializeComponent();
        }
        
        private void LoadStation()
        {
            Station.Value = StationId is null ? null : Bl.GetBaseStation(StationId.Value);

            NewName.Value = Station.Value?.Name ?? string.Empty;
            NewNumChargingSlots.Value = (ushort?) Station.Value?.AvailableChargingSlots ?? 0;
        }

        /// <summary>
        /// Called when the station is updated. EventArgs is the ID of
        /// the station. 
        /// </summary>
        public event EventHandler<int>? StationUpdated;
        
        private void UpdateStationButton_Click(object sender, RoutedEventArgs e)
        {
            Bl.UpdateBaseStation(Station.Value!.Id, NewName.Value, NewNumChargingSlots.Value);
            StationUpdated?.Invoke(this, Station.Value.Id);
        }

        private void ShowDrone(object sender, MouseButtonEventArgs e)
        {
            var id = (int)((ListView) sender).SelectedValue;
            ((MainWindow)Application.Current.MainWindow!).ShowDrone(id);
        }
    }
}