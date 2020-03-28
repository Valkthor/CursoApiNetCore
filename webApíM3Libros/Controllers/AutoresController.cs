using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApíM3Libros.Contexts;
using webApíM3Libros.Entities;
using Microsoft.EntityFrameworkCore;

namespace webApíM3Libros.Controllers
{
    //se usa api antres por convencion
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        //inyectando el application db context en autores controller
        // para eso se crea una instancia
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //despues se crean los metodos que se van a utilizar

        [HttpGet]

        //actionResult indica los tipos de recursos que puede retornar
        public ActionResult<IEnumerable<Autor>> Get()
        {
            //lo que hace es traer los autores de la base de datos
            //esto es una programacion sincronica, despues se enseñara a utilizar la opcion asincrona
            return context.Autores.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public ActionResult<Autor> Get(int id)
        {
            //filtro en la bd el dato segun el id
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);

            //se valida que traiga datos
            if (autor == null)
            {
                // se retorna 404
                return NotFound();
            }

            return autor;
        }

        // se obtienen valores a traves de la url ej api/2/ricardo
        /*
        [HttpGet("[{id}/param2=perez")]
        public Autor Get(int id, string param2)
        {
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);
            //obtiene el dato del autor para mostrarlo
            return autor;
        }
        */

        [HttpGet("[{id}/param2=perez")]
        public ActionResult<Autor> Get(int id, string param2)
        {
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);
            
            // al agregar action result, se puede validar a diferencia de la funcion de arriba.
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }
        [HttpGet("asincrona/{id}/{param3}")]
        public async Task<ActionResult<Autor>> Get(int id, bool param3)
        {
            // se espera una respuesta con el await a la operacion asincrona.
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            // al agregar action result, se puede validar a diferencia de la funcion de arriba.
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        <
        // aca se hace un insert, para eso se le indica que tiene que leer el cuerpo de la peticion http
        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            
            context.Autores.Add(autor);
            // se guardan los cambios
            context.SaveChanges();

            //se retorna una ruta en donde el cliente puede ver el recurso.
            // para esto se configura el name en el getID
            return new CreatedAtRouteResult("ObtenerAutor",
                   // se re manda el id al recurso llamado obtenerAutor
                   new { id = autor.Id },
                   autor
                );
        }

        [HttpPut("{id}")]

        public ActionResult Put(int id, [FromBody] Autor value)
        {

            if (id != value.Id )
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
