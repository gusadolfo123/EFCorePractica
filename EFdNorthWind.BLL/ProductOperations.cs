namespace EFdNorthWind.BLL
{
    using EFdNorthWind.DAL;
    using EFdNorthWind.Entities;
    using EFdNorthWind.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

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

        public List<Product> GetAll(QueryParameters<Product> queryParameters = null)
        {
            return NorthWindRepositoryFactory.GetNorthWindRepository().GetProducts(queryParameters);
        }

        public Product RetrieveByID(int productID, QueryParameters<Product> queryParameters = null)
        {
            Product product = null;
            if (productID > 0)
            {
                using (var repository = NorthWindRepositoryFactory.GetNorthWindRepository())
                {
                    product = repository.RetrieveProductByID(productID, queryParameters);
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
