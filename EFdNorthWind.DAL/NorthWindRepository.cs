namespace EFdNorthWind.DAL
{
    using EFdNorthWind.Entities;
    using EFdNorthWind.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    public class NorthWindRepository : INorthWindRepository
    {
        readonly EFNorthWindContext _context;
        readonly bool IsUnitOfWork;

        public NorthWindRepository(bool isUnitOfWork = false)
        {
            _context = new EFNorthWindContext();
            IsUnitOfWork = isUnitOfWork;
        }

        public Category CreateCategory(Category category)
        {
            category = _context.Add(category).Entity;
            Save();
            return category;
        }

        public Log CreateLog(Log log)
        {
            log = _context.Add(log).Entity;
            Save();
            return log;
        }

        public bool DeleteCategory(int categoryID)
        {
            _context.Remove(new Category { CategoryID = categoryID });
            return Save();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public List<Log> GetLogs()
        {
            return _context.Logs.ToList();
        }

        public Category RetrieveCategoryByID(int categoryID)
        {
            return _context.Find<Category>(categoryID);            
        }

        public int SaveChanges()
        {
            var result = 0;
            if (_context != null)
            {
                try
                {
                    result = _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        private bool Save()
        {
            int changes = 0;
            if (!IsUnitOfWork)
            {
                changes = SaveChanges();
            }
            return changes == 1;    
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Product CreateProduct(Product product)
        {
            product = _context.Add(product).Entity;
            Save();
            return product;
        }

        public Product RetrieveProductByID(int productID, QueryParameters<Product> queryParameters = null)
        {
            var query = _context.Set<Product>().AsQueryable();
            
            if (queryParameters.Includes != null)
            {
                foreach (var include in queryParameters.Includes)
                {
                    query = query.Include(include);
                }
            }

            var product = query.Where(p => p.ProductID == productID).FirstOrDefault();

            return product;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }

        public bool DeleteProduct(int productID)
        {
            _context.Remove(new Product { ProductID = productID });
            return Save();
        }

        public List<Product> GetProducts(QueryParameters<Product> queryParameters = null)
        {
            var query = _context.Set<Product>().AsQueryable();

            if (queryParameters != null)
            {
                if (queryParameters.Includes != null)
                {
                    foreach (var include in queryParameters.Includes)
                    {
                        query = query.Include(include);
                    }
                }
                return query.Where(queryParameters.Where).ToList();
            }

            return query.ToList();
        }
    }
}
