namespace EFdNorthWind.BLL
{
    using EFdNorthWind.DAL;
    using EFdNorthWind.Entities;
    using EFdNorthWind.Services;
    using System.Collections.Generic;

    public class ProductOperations : IProductOperations
    {
        public Product Create(Product product)
        {
            if (!string.IsNullOrWhiteSpace(product.ProductName))
            {
                using (var repository = NorthWindRepositoryFactory.GetNorthWindRepository())
                {
                    product = repository.CreateProduct(product);
                }
            }
            else
            {
                product = null;
            }
            return product;
        }

        public bool Delete(int productID)
        {
            bool result = false;
            using (var repository = NorthWindRepositoryFactory.GetNorthWindRepository())
            {
                result = repository.DeleteProduct(productID);
            }
            return result;
        }

        public bool DeleteWithLog(int productID)
        {
            bool result = false;
            using (var repository = NorthWindRepositoryFactory.GetNorthWindRepository(true))
            {
                repository.DeleteProduct(productID);

                var log = new Log
                {
                    Type = LogType.DeleteProduct,
                    Message = $"Producto Eliminado ID: {productID}"
                };

                repository.CreateLog(log);

                result = repository.SaveChanges() == 2;
            }
            return result;
        }

        public List<Product> GetAll()
        {
            return NorthWindRepositoryFactory.GetNorthWindRepository().GetProducts();
        }

        public Product RetrieveByID(int productID)
        {
            Product product = null;
            if (productID > 0)
            {
                using (var repository = NorthWindRepositoryFactory.GetNorthWindRepository())
                {
                    product = repository.RetrieveProductByID(productID);
                }
            }
            return product;
        }

        public bool Update(Product product)
        {
            bool result = false;
            using (var repository = NorthWindRepositoryFactory.GetNorthWindRepository())
            {
                result = repository.UpdateProduct(product);
            }
            return result;
        }
    }
}
