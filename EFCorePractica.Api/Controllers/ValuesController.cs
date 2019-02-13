using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFdNorthWind.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePractica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// Get Para Todos los Productos
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        /// <summary>
        /// Get Para Producto Especifico
        /// </summary>
        /// <param name="id">Id del Producto</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Post encargado de recibir y devolver un producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetProduct")]
        public ActionResult<Product> GetProduct([FromBody] Product product)
        {
            return product;
        }

        // POST api/values
        /// <summary>
        /// Post de Creacion de Producto
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]        
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
