using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for PackagesListWindow.xaml
    /// </summary>
    public partial class PackagesListWindow : Window
    {
        private readonly BlApi.IBL bl;

        public IEnumerable<PackageListing> Packages { get; private set; }

        public PackagesListWindow(BlApi.IBL bl)
        {
            InitializeComponent();

            this.bl = bl;

            Packages = bl.GetPackageList();
        }

        private void NewPackage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //new AddPackageWindow(this).ShowDialog();
        }

        private void NewPackage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PackagesListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
           
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
