using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo7.Models;

namespace WebApiModulo7.Contexts
{
    public class MyApplicationDbContext: IdentityDbContext<MyApplicationUser>
    {
        public MyApplicationDbContext(DbContextOptions<MyApplicationDbContext> options)
            :base(options)
        {

        }

        // aca se podria colocar los dbs de las tablasdel negocio, probare.

        public int idEmpresa { get; set; }

        public int idUsuario { get; set; }



    }
}
