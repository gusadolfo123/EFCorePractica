namespace EFdNorthWind.Services
{
    using EFdNorthWind.Entities;
    using System.Collections.Generic;

    public interface IProductOperations
    {
        Product Create(Product product);

        Product RetrieveByID(int productID);

        bool Update(Product product);

        bool Delete(int productID);

        List<Product> GetAll();

        bool DeleteWithLog(int categoryID);

    }
}
