using System;
using System.Windows;
using System.Windows.Controls;
using BL;

namespace PL
{
    public partial class UpdatePackageControl : UserControl
    {
        public Package Package
        {
            get => (Package) GetValue(PackageProperty);
            set => SetValue(PackageProperty, value);
        }

        public static readonly DependencyProperty PackageProperty =
            DependencyProperty.Register("Package", typeof(Package), typeof(UpdatePackageControl),
                new PropertyMetadata(default(Package)));

        public UpdatePackageControl()
        {
            InitializeComponent();
        }
    }
}