using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApíM3Libros.Contexts;
using webApíM3Libros.Entities;

namespace webApíM3Libros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            //obtiene el dato del autor para mostrarlo
            return context.Libros.Include(x=> x.Autor).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);

            //se valida que traiga datos
            if (libro == null)
            {
                // se retorna 404
                return NotFound();
            }

            return libro;
        }

        // aca se hace un insert, para eso se le indica que tiene que leer el cuerpo de la peticion http
        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro",
       
                   new { id = libro.Id },
                   libro
                );
        }

        [HttpPut("{id}")]

        public ActionResult Put(int id, [FromBody] Autor value)
        {

            if (id != value.Id)
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return BadRequest();
            }

            context.Autores.Remove(autor);
            context.SaveChanges();
            return Ok();
        }

    }
}
