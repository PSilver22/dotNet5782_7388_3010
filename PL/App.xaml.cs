using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MapControl;

namespace PL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        static App()
        {
            ImageLoader.HttpClient.DefaultRequestHeaders.Add("User-Agent", "School Project");
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
