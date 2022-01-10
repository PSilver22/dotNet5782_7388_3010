#nullable enable

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PL.Utilities;
using BL;
using DO;
using PL.PackageList;

namespace PL
{
    /// <summary>
    /// Interaction logic for PackagesListWindow.xaml
    /// </summary>
    public partial class PackagesListWindow : Window
    {
        private readonly BlApi.IBL _bl;

        public ObservableCollection<PackageListing> Packages { get; private set; }
        public Prop<BL.Package?> SelectedPackage { get; } = new();

        public PackageListing? SelectedPackageListing
        {
            get =>
                SelectedPackage.Value == null
                    ? null
                    : Packages.FirstOrDefault(p => p.Id == SelectedPackage.Value.Id);
            set => SelectedPackage.Value = value == null ? null : _bl.GetPackage(value.Id);
        }

        public WeightCategory? SelectedWeight { get; set; }
        public Priority? SelectedPriority { get; set; }
        public PackageStatus? SelectedStatus { get; set; }

        public PackagesListWindow(BlApi.IBL bl)
        {
            _bl = bl;

            Packages = new ObservableCollection<PackageListing>(bl.GetPackageList());

            CollectionViewSource.GetDefaultView(Packages).Filter = ListFilter;

            InitializeComponent();
        }

        private bool ListFilter(object item)
        {
            var incl = true;
            if (SelectedWeight.HasValue)
                incl &= (item as PackageListing)!.Weight == SelectedWeight.Value;
            if (SelectedPriority.HasValue)
                incl &= (item as PackageListing)!.Priority == SelectedPriority.Value;
            if (SelectedStatus.HasValue)
                incl &= (item as PackageListing)!.Status == SelectedStatus.Value;
            return incl;
        }

        private void NewPackage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var apw = new AddPackageWindow(_bl);
            apw.PackageAdded += id =>
            {
                var newPackage = _bl.GetPackageList().First(p => p.Id == id);
                Packages.Add(newPackage);
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
    }
}