namespace EFdNorthWind.Services
{
    using EFdNorthWind.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IProductOperations
    {
        Product Create(Product product);

        Product RetrieveByID(int productID, QueryParameters<Product> queryParameters = null);

        bool Update(Product product);

        bool Delete(int productID);

        List<Product> GetAll(QueryParameters<Product> queryParameters = null);

        bool DeleteWithLog(int productID); 

    }
}
