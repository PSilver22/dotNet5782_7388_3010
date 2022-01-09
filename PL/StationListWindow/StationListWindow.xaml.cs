using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
    {
        private enum ChargeSlotFilter { All = 0, Available = 1, Unavailable = 2 }

        private readonly BlApi.IBL bl;
        
        public StationListWindow(BlApi.IBL bl)
        {
            InitializeComponent();

            this.bl = bl;

            ChargeSlotsFilterComboBox.ItemsSource = from filter in (IEnumerable<ChargeSlotFilter>)Enum.GetValues(typeof(ChargeSlotFilter))
                                                    select new ComboBoxItem() { Content = filter.ToString(), Tag = filter };
            
            ChargeSlotsFilterComboBox.SelectedValue = ChargeSlotFilter.All;

            ReloadStations();
        }

        /// <summary>
        /// Updates the station list graphics to match the IBL station list
        /// </summary>
        public void ReloadStations() {
            int statusTag = (int)(ChargeSlotsFilterComboBox.SelectedValue);

            StationsListView.ItemsSource = (statusTag == 0) ?
                bl.GetBaseStationList() :
                    (statusTag == 1) ?
                    bl.GetBaseStationList(s => s.AvailableChargingSlotsCount > 0) :
                    bl.GetBaseStationList(s => s.AvailableChargingSlotsCount == 0);

            UpdateStationDisplay();
        }

        /// <summary>
        /// Updates the info on the UpdateStationsPage
        /// </summary>
        private void UpdateStationDisplay() {
            if (StationsListView.SelectedItem is not null)
            {
                BL.BaseStation bs = bl.GetBaseStation(((BL.BaseStationListing)StationsListView.SelectedItem).Id);
                
                if (PageDisplay.Content is null)
                {
                    ((UpdateStationPage)(PageDisplay.Content = new UpdateStationPage(this, bs))).AddObserver(this);
                }
                else
                {
                    if (StationsListView.Tag is null || (int)StationsListView.Tag != bs.Id)
                    {
                        ((UpdateStationPage)PageDisplay.Content).SetStation(bs, notify: false);
                    }
                }
            }
            else {
                PageDisplay.Content = null;
            }

            StationsListView.Tag = ((UpdateStationPage)PageDisplay.Content)?.Station.Id;
        }

        /// <summary>
        /// Executed method for New command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewStation_Executed(object sender, ExecutedRoutedEventArgs e) {
            new AddStationWindow(this).ShowDialog();
        }

        /// <summary>
        /// CanExecute method for New command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewStation_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        /// <summary>
        /// Executed method for Refresh command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshStations_Executed(object sender, ExecutedRoutedEventArgs e) {
            ReloadStations();
        }

        /// <summary>
        /// CanExecute method for Refresh command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshStations_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        /// <summary>
        /// Selection changed event for StationsListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStationDisplay();
        }

        /// <summary>
        /// Selection changed event for FilterComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ReloadStations();
        }
    }
}
