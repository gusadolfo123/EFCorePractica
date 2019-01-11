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
        ICategoryOperations Helper;

        public CategoriesController(ICategoryOperations categoryOperations)
        {
            Helper = categoryOperations;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            IActionResult Result;

            category = Helper.Create(category);

            if (category != null)
            {
                Result = Content($"Categoria Insertada ${category.CategoryID}");
            }
            else
            {
                Result = Content($"No se Pudo Insertar la Categoria");
            }

            return Result;
        }

        public IActionResult Retrieve(int id)
        {
            IActionResult Result;
            var category = Helper.RetrieveByID(id);

            if (category != null)
            {
                Result = Content($"Categoria Encontrada ${category.CategoryID} ${category.CategoryName}");
            }
            else
            {
                Result = Content($"No se Pudo Encontrar la Categoria");
            }

            return Result;
        }

        public IActionResult Update(int id, string name, string description)
        {
            IActionResult Result;
            var category = new Category
            {
                CategoryID = id,
                CategoryName = name,
                Description = description
            };

            var updateResult = Helper.Update(category);

            if (updateResult)
            {
                Result = Content($"Categoria Modificada");
            }
            else
            {
                Result = Content($"No se Pudo Modificar la Categoria");
            }

            return Result;
        }

        public IActionResult Delete(int id, bool withLog = false)
        {
            IActionResult Result;
            var DeleteResult = withLog ? Helper.DeleteWithLog(id) : Helper.Delete(id);

            if (DeleteResult)
            {
                Result = Content($"Categoria Eliminada");
            }
            else
            {
                Result = Content($"No se Pudo Eliminar la Categoria");
            }
            return Result;
        }

        public IActionResult All()
        {
            var model = Helper.GetAll();
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}