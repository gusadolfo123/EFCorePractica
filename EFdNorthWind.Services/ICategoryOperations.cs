namespace EFdNorthWind.Services
{
    using EFdNorthWind.Entities;
    using System.Collections.Generic;

    public interface ICategoryOperations
    {
        Category Create(Category category);

        Category RetrieveByID(int categoryID);

        bool Update(Category category);

        bool Delete(int categoryID);

        List<Category> GetAll();

        bool DeleteWithLog(int categoryID);
    }
}
