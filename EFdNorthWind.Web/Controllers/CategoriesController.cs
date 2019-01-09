namespace EFdNorthWind.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Entities;

    public class CategoriesController : Controller
    {
        private readonly ICategoryOperations Helper;

        public CategoriesController(ICategoryOperations categoryOperations)
        {
            Helper = categoryOperations;
        }

        public IActionResult Create(string name, string description)
        {
            IActionResult Result;

            var category = new Category
            {
                CategoryName = name,
                Description = description
            };

            category = Helper.Create(category);

            if(category != null)
            {
                Result = Content($"Categoria Insertada ${category.CategoryID}");
            }
            else
            {
                Result = Content($"No se Pudo Insertar la Categoria");
            }

            return Result;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}