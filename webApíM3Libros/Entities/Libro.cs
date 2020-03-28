using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApíM3Libros.Entities
{
    public class Libro
    {
        public int Id { get; set; }

        //se indica que esta propiedad es requerida al momento de recibir parametros por post
        [Required]
        public string Titulo { get; set; }
        [Required]
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
