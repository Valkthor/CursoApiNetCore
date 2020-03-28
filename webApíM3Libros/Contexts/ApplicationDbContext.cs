
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApíM3Libros.Entities;

namespace webApíM3Libros.Contexts
{
    // aca se configuran las distintas tablas de la base de datos
    public class ApplicationDbContext: DbContext
    {
        //se crea consturctor en donde se pasa un dbcontextOptions
        //y a la clase se le pasan esas opciones
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {

        }

        //la clase autor se va corresponder con una tabla en la base de datos
        //por eso se crea esta propiedad, el nombre de la tabla se va a llamar autores
        // se tiene que agregar el dbset correspondiente en el using
        // en nuestroa base existira una tabla que se llama autores, 
        // cuyo esquema va a ser copiado de las propiedades de la clase autor
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }

    }
}
