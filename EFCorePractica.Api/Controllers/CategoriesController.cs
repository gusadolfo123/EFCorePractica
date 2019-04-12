using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EFdNorthWind.Entities;
using EFdNorthWind.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePractica.Api.Controllers
{

    /// <summary>
    /// Controlador encargado de realizar las acciones correspondientes para la entidad Category
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {

        public ICategoryOperations Helper { get; set; }

        public CategoriesController(ICategoryOperations categoryOperations)
        {
            this.Helper = categoryOperations;
        }

        // GET: api/Categories
        /// <summary>
        /// Metodo encargado de obtener el total de categorias 
        /// </summary>
        /// <returns>Listado de Categorias</returns>
        [HttpGet]        
        public List<Category> GetAll()
        {
            return this.Helper.GetAll();
        }

        // GET: api/Categories/5
        /// <summary>
        /// Metodo encargado de obtener una categoria con criterio de busqueda por CategoryID
        /// </summary>
        /// <param name="id">Indetificador unico de categoria CategoryID</param>
        /// <returns>Categoria Encontrada</returns>
        [HttpGet("{id}", Name = "Get")]
        public Category Get(int id)
        {
            return this.Helper.RetrieveByID(id);
        }

        // POST: api/Categories
        [HttpPost("[action]")]        
        public IActionResult Create([FromBody] Category category)
        {
            category = this.Helper.Create(category);
            if (category != null)
                return Ok(category);
            else
                return BadRequest();
        }

        // PUT: api/Categories/5
        [HttpPut("[action]")]
        public IActionResult Update([FromBody] Category category)
        {
            if (this.Helper.Update(category))
                return Ok(category);
            else
                return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (this.Helper.Delete(id))
                return Ok();
            else
                return BadRequest();
        }
    }
}
