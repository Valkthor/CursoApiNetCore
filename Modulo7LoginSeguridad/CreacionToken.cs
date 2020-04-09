using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiModulo7.Models;

namespace Modulo7LoginSeguridad
{
    public class CreacionToken
    {
        public readonly IConfiguration _config;

        public CreacionToken(IConfiguration configuration)
        {
            _config = configuration;
        }
        public MyUserToken MyBuildToken(MyUserInfo userInfo, IList<string> roles)
        {
            //los claims son datos confiables que van a viajar con el token
            var claims = new List<Claim>
            {
                //requere la instalaccion del nugget para jwt
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),

                // se pueden agregar valores personalizados
                new Claim("miValor", "Lo que yo quiera"),

                //Jti identifica de manera unica un token, se puede usar para invalidar un token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //esto se agrego despues del original, agregue una validacion para los roles, si tiene datos entonces agrega el claim del role.
            if (roles.Count > 0)
            {
                foreach (var rol in roles)
                {
                    // se agrega al claim todos los roles al cual pertenece el usuario
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }


            // se crean las llaves para el token, esta esta en la configuracion, seria ideal en una variable de ambiente
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]));

            //se instalan credenciales.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora. puede ser minutos segundos meses etc.
            var expiration = DateTime.UtcNow.AddMinutes(3);

            //se instancia el token de seguridad
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            // se instancia la clase con el usuario y token, se crea en la misma linea
            return new MyUserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
