namespace EFdNorthWind.WPF
{
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using EFdNorthWind.Entities;
    using EFdNorthWind.BLL;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
     
        private void Product(object sender, RoutedEventArgs e)
        {
            var FrmProduct = new ProductWindow();
            FrmProduct.Show();
        }

        private void Category(object sender, RoutedEventArgs e)
        {
            var FrmCategory = new CategoryWindow();
            FrmCategory.Show();
        }
    }
}
