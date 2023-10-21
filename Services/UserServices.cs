using CompraVentaCarrosApi.Models;
using CompraVentaCarrosApi.Models.Commons;
using CompraVentaCarrosApi.Models.Request;
using CompraVentaCarrosApi.Models.Response;
using CompraVentaCarrosApi.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly AppSettings _appSettings;

        public UserServices(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse oUserResponse = new UserResponse();
            using (var db = new VENTACARROSContext())
            {
                string password = Encrypt.GetSHA256(model.Contraseña);
                var oUser = (from d in db.Usuarios
                             where d.Usuario1 == model.Usuario1.Trim() && d.Contraseña == model.Contraseña.Trim()
                             select d).FirstOrDefault();

                var usuario = db.Usuarios.Where(d => d.Usuario1 == model.Usuario1 &&
                                                d.Contraseña == password).FirstOrDefault();
                if (usuario == null) return null;
                var persona = db.Personas.Where(d => d.Id == usuario.IdPersona).FirstOrDefault();
                var rol = db.Rols.Where(d => d.Id == usuario.IdRol).FirstOrDefault();

               
                oUserResponse.User = usuario.Usuario1;
                oUserResponse.token = GetToken(usuario,persona,rol);
            }
            return oUserResponse;
        }
        private string GetToken(Usuario usuario, Persona persona, Rol rol)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, $"{persona.Nombre} {persona.Apellido}"),
                        new Claim(ClaimTypes.Role, rol.Name),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                        

                    }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
