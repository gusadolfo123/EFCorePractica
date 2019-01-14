namespace EFdNorthWind.WPF
{
    
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
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
    using EFdNorthWind.BLL;
    using EFdNorthWind.Entities;

    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {

        public ProductWindow()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            var Helper = OperarionsFactory.GetCategoryOperations();
            Category.ItemsSource = Helper.GetAll();
            Category.SelectedValuePath = "CategoryID";
            Category.DisplayMemberPath = "CategoryName";
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetProductsOperations();
            var category = Category.SelectedItem as Category;

            var product = Helper.Create(new Product
            {
                ProductName = ProductName.Text,
                UnitPrice = decimal.Parse(UnitPrice.Text),
                UnitsInStock = int.Parse(UnitsInStock.Text),
                CategoryID = category.CategoryID
            });

            string Result;

            if (product != null)
            {
                Result = $"Producto Insertado {product.ProductID}";
                ProductID.Text = product.ProductID.ToString();
            }
            else
            {
                Result = $"No se Pudo Insertar el Producto";
            }

            MessageBox.Show(Result);

            Data.ItemsSource = GetProductsFilter()
                                   .Select(c => new { c.ProductID, c.ProductName, c.UnitPrice, c.UnitsInStock, c.Category.CategoryName })
                                   .ToList();

        }

        private void Retrieve(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetProductsOperations();
            var productID = ProductID.Text != "" ? int.Parse(ProductID.Text) : 0;

            var product = Helper.RetrieveByID(productID, new QueryParameters<Product> {
                Includes = new List<Expression<Func<Product, object>>> { x => x.Category }
            });
                        
            if (product != null)
            {
                ProductName.Text = product.ProductName;
                UnitPrice.Text = product.UnitPrice.ToString();
                UnitsInStock.Text = product.UnitsInStock.ToString();
                Category.SelectedValue = product.CategoryID;
            }
            else
            {
                MessageBox.Show("Producto no Encontrado");
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetProductsOperations();
            var category = Category.SelectedItem as Category;

            var product = new Product
            {
                ProductID = int.Parse(ProductID.Text),
                ProductName = ProductName.Text,
                UnitPrice = decimal.Parse(UnitPrice.Text),
                UnitsInStock = int.Parse(UnitsInStock.Text),
                CategoryID = category.CategoryID
            };

            var resUpdate = Helper.Update(product);
            string Result;

            if (product != null)
            {
                Result = $"Producto Modificado {product.ProductID}";
                ProductID.Text = product.ProductID.ToString();
            }
            else
            {
                Result = $"No se Pudo Modificar el Producto";
            }

            MessageBox.Show(Result);

            Data.ItemsSource = GetProductsFilter()
                                   .Select(c => new { c.ProductID, c.ProductName, c.UnitPrice, c.UnitsInStock, c.Category.CategoryName })
                                   .ToList();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetProductsOperations();
            int ID = int.Parse(ProductID.Text);

            var DeleteResult = WithLog.IsChecked.Value ? Helper.DeleteWithLog(ID) : Helper.Delete(ID);
            string Result;

            if (DeleteResult)
            {
                Result = $"Producto Eliminado";
            }
            else
            {
                Result = $"No se Pudo Eliminar el Producto";
            }

            MessageBox.Show(Result);

            Data.ItemsSource = GetProductsFilter()
                                   .Select(c => new { c.ProductID, c.ProductName, c.UnitPrice, c.UnitsInStock, c.Category.CategoryName })
                                   .ToList();

        }

        private void GetProducts(object sender, RoutedEventArgs e)
        {
            Data.ItemsSource = GetProductsFilter()
                                    .Select(c => new { c.ProductID, c.ProductName, c.UnitPrice, c.UnitsInStock, c.Category.CategoryName })
                                    .ToList();

            Data.Visibility = Visibility.Visible;
        }

        private void GetProductsByCartegory(object sender, RoutedEventArgs e)
        {
            var categoryID = (Category.SelectedItem is Category category ? category.CategoryID : 0);
            Data.ItemsSource = GetProductsFilter(categoryID)
                                   .Select(c => new { c.ProductID, c.ProductName, c.UnitPrice, c.UnitsInStock, c.Category.CategoryName })
                                   .ToList();

            Data.Visibility = Visibility.Visible;
        }

        private void GetLogs(object sender, RoutedEventArgs e)
        {
            var Helper = OperarionsFactory.GetLogOperations();
            Data.ItemsSource = Helper.GetAll();
            Data.Visibility = Visibility.Visible;
        }

        private List<Product> GetProductsFilter(int categoryID = 0)
        {
            var Helper = OperarionsFactory.GetProductsOperations();
            var data = Helper.GetAll(new QueryParameters<Product>
            {
                Includes = new List<Expression<Func<Product, object>>> { x => x.Category },
                Where = x => x.CategoryID == (categoryID != 0 ? categoryID : x.CategoryID)
            });

            return data.ToList();
        }
                
       
    }
}
