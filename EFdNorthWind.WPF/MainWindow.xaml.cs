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

        private void Create(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetCategoryOperations();

            var category = new Category
            {
                CategoryName = CategoryName.Text,
                Description = Description.Text
            };

            category = Helper.Create(category);
            string Result;

            if (category != null)
            {
                Result = $"Categoria Insertada {category.CategoryID}";
                CategoryID.Text = category.CategoryID.ToString();
            }
            else 
            {
                Result = $"No se Pudo Insertar la Categoria";
            }

            MessageBox.Show(Result);
        }

        private void Retrieve(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetCategoryOperations();
            
            var category = Helper.RetrieveByID(int.Parse(CategoryID.Text));
            string Result;

            if (category != null)
            {
                CategoryName.Text = category.CategoryName;
                Description.Text = category.Description;
            }
            else
            {
                Result = $"No se Pudo Obtener la Categoria";
                MessageBox.Show(Result);
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetCategoryOperations();
            var category = new Category
            {
                CategoryID = int.Parse(CategoryID.Text),
                CategoryName = CategoryName.Text,
                Description = Description.Text
            };

            var resultUpdate = Helper.Update(category);
            string Result;

            if (resultUpdate)
            {
                Result = $"Categoria Modificada";
            }
            else
            {
                Result = $"No se Pudo Modificar la Categoria";
            }
            MessageBox.Show(Result);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetCategoryOperations();
            int ID = int.Parse(CategoryID.Text);

            var DeleteResult = WithLog.IsChecked.Value ? Helper.DeleteWithLog(ID) : Helper.Delete(ID);
            string Result;

            if (DeleteResult)
            {
                Result = $"Categoria Eliminada";
            }
            else
            {
                Result = $"No se Pudo Eliminar la Categoria";
            }
        }

        private void GetCategories(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetCategoryOperations();
            Data.ItemsSource = Helper.GetAll()
                .Select(c => new { c.CategoryID, c.CategoryName, c.Description });

            Data.Visibility = Visibility.Visible;
        }

        private void GetLogs(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetLogOperations();
            Data.ItemsSource = Helper.GetAll();
            Data.Visibility = Visibility.Visible;
        }
    }
}
