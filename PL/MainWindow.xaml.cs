using System.Windows;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBL Bl { get; set; }

        public MainWindow()
        {
            Bl = BLFactory.GetBl();
            InitializeComponent();
        }
    }
}
