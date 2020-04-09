using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Modulo7LoginSeguridad.Models;
using WebApiModulo7.Contexts;
using WebApiModulo7.Models;

namespace Modulo7LoginSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private MyApplicationDbContext _MyContext;
        private UserManager<MyApplicationUser> _MyUserManager;

        public UsuariosController( MyApplicationDbContext myContext,
                                   UserManager<MyApplicationUser> MyUserManager )
        {
            this._MyContext = myContext;
            this._MyUserManager = MyUserManager;

        }

        [Route("AsignarUsuarioRol")]
        public async Task<ActionResult> AsignarRolUsuario(MyEditarRolDTO editarRolDTO)
        {
            //se busca al usuario por su id
            var usuario = await _MyUserManager.FindByIdAsync(editarRolDTO.UserID);

            // se valida que el usuario alla sido encontrado
            if (usuario == null) { return NotFound(); }

            // se agregan las dos formas los mismos datos, este es para la identificacion clasica
            await _MyUserManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));

            // este es para JWT
            await _MyUserManager.AddToRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }


        [Route("RemoverUsuarioRol")]
        public async Task<ActionResult> RemoverRolUsuario(MyEditarRolDTO editarRolDTO)
        {
            //se busca al usuario por su id
            var usuario = await _MyUserManager.FindByIdAsync(editarRolDTO.UserID);

            // se valida que el usuario alla sido encontrado
            if (usuario == null) { return NotFound(); }

            // se agregan las dos formas los mismos datos, este es para la identificacion clasica
            await _MyUserManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));

            // este es para JWT
            await _MyUserManager.RemoveFromRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }
    }
}
