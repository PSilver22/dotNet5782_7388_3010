#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BL;
using DO;

namespace PL
{
    /// <summary>
    /// Interaction logic for PackagesListWindow.xaml
    /// </summary>
    public partial class PackagesListWindow : Window
    {
        private readonly BlApi.IBL bl;

        public ObservableCollection<PackageListing> Packages { get; }
        public Prop<BL.Package?> SelectedPackage { get; } = new();

        public PackageListing? SelectedPackageListing
        {
            get
            {
                return SelectedPackage.Value == null
                    ? null
                    : Packages.FirstOrDefault(p => p.Id == SelectedPackage.Value.Id);
            }
            set
            {
                SelectedPackage.Value = bl.GetPackage(value.Id);
                //SelectedPackage.Value = value;
            }
        }

        public WeightCategory? SelectedWeight { get; set; }
        public Priority? SelectedPriority { get; set; }
        public PackageStatus? SelectedStatus { get; set; }

        public PackagesListWindow(BlApi.IBL bl)
        {
            this.bl = bl;

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
            //new AddPackageWindow(this).ShowDialog();
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
