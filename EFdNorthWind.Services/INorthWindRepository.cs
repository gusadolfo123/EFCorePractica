namespace EFdNorthWind.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using EFdNorthWind.Entities;

    public interface INorthWindRepository: IDisposable 
    {
        Category CreateCategory(Category category);

        Category RetrieveCategoryByID(int categoryID);

        bool UpdateCategory(Category category);

        bool DeleteCategory(int categoryID);

        List<Category> GetCategories();


        Product CreateProduct(Product product);

        Product RetrieveProductByID(int productID, QueryParameters<Product> queryParameters = null);

        bool UpdateProduct(Product product);

        bool DeleteProduct(int productID);

        List<Product> GetProducts(QueryParameters<Product> queryParameters = null); 
        

        List<Log> GetLogs();

        Log CreateLog(Log log);

        int SaveChanges();
    }
}
