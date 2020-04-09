using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiModulo7.Models;

namespace Modulo7LoginSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<MyApplicationUser> _userManager;
        private readonly SignInManager<MyApplicationUser> _signInManager;
        public readonly IConfiguration _configuration;
        private readonly CreacionToken objBuildToken;

        //constructor
        public CuentasController(UserManager<MyApplicationUser> userManager,
                                    SignInManager<MyApplicationUser> signInManager,
                                    IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

            //esto lo hice para separar la funcion que crea el token, del controller
            objBuildToken = new CreacionToken(configuration);
        }

        [HttpPost("Crear")]
        //aca el usuario manda su usuario y password
        public async Task<ActionResult<MyUserToken>> CreateUser([FromBody] MyUserInfo UserInfoModel)
        {
            //aca se crea el usuario
            var user = new MyApplicationUser { UserName = UserInfoModel.Email, Email = UserInfoModel.Email };
            var result = await _userManager.CreateAsync(user, UserInfoModel.Password);

            if (result.Succeeded)
            {
                return objBuildToken.MyBuildToken(UserInfoModel, new List<string>());
            }
            else
            {
                return BadRequest("Username or password invalid");
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<MyUserToken>> Login([FromBody] MyUserInfo userInfoModel)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfoModel.Email, userInfoModel.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var usuario = await _userManager.FindByEmailAsync(userInfoModel.Email);
                var roles = await _userManager.GetRolesAsync(usuario);
                return objBuildToken.MyBuildToken(userInfoModel, roles);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }


    }
}
