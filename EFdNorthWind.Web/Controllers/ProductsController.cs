namespace EFdNorthWind.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using EFdNorthWind.Entities;
    using EFdNorthWind.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProductsController : Controller
    {

        private readonly IProductOperations Helper;
        private readonly ICategoryOperations HelperCategory;

        public ProductsController(IProductOperations productOperations, ICategoryOperations categoryOperations)
        {
            Helper = productOperations;
            HelperCategory = categoryOperations;
        }

        // GET: Products
        public ActionResult Index() 
        {
            var listProducts = Helper.GetAll(new QueryParameters<Product> 
                { 
                    Includes = new List<Expression<Func<Product, object>>> 
                                { 
                                    x => x.Category
                                }
                }).ToList();
            return View(listProducts);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            var product = Helper.RetrieveByID(id, new QueryParameters<Product>
            {
                Includes = new List<Expression<Func<Product, object>>>
                {
                    x => x.Category
                }
            });

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(HelperCategory.GetAll().ToList(), "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                // TODO: Add insert logic here
                product = Helper.Create(product);
                
                if (product != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Categories = new SelectList(HelperCategory.GetAll().ToList(), "CategoryID", "CategoryName", product.CategoryID);
                    return View(product);
                }
            }
            catch
            {
                ViewBag.Categories = new SelectList(HelperCategory.GetAll().ToList(), "CategoryID", "CategoryName", product.CategoryID);
                return View(product);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var product = Helper.RetrieveByID(id, new QueryParameters<Product>
            {
                Includes = new List<Expression<Func<Product, object>>>
                {
                    x => x.Category
                }
            });

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                // TODO: Add update logic here
                var resultUpdate = Helper.Update(product);
                if (resultUpdate)
                {
                    return RedirectToAction(nameof(Details), product.ProductID);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}