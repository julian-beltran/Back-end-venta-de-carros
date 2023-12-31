﻿using CompraVentaCarrosApi.Models;
using CompraVentaCarrosApi.Models.Commons;
using CompraVentaCarrosApi.Models.Request;
using CompraVentaCarrosApi.Models.Response;
using CompraVentaCarrosApi.Services;
using CompraVentaCarrosApi.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace CompraVentaCarrosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IUserServices _userServices;
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailServices;
        public IAzureStorage _azureStorage;
        public AuthController(IUserServices userServices, IConfiguration configuration, IEmailServices emailService, IAzureStorage azureStorage)
        {
            _userServices = userServices;
            _configuration = configuration;
            _emailServices = emailService;
            _azureStorage = azureStorage;
            
        }

        // POST api/<AuthController>
        [HttpPost("login")]
        public IActionResult Authentication([FromBody] AuthRequest model)
        {
            Respuesta respuesta = new Respuesta();
            var userresponse = _userServices.Auth(model);
            if(userresponse == null)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o contraseña incorrecta";
                return Ok(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.Data = userresponse;
            respuesta.Mensaje = "Usuario ingreso con exito";
            return Ok(respuesta );
        }

        // GET api/<AuthController>
        [HttpPost("Register")]
        public IActionResult Register([FromForm] RegisterRequest oModel)
        {
            
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            Persona oPersona = new Persona();
            Usuario oUsuario = new Usuario();
            Rol rol = new Rol();
            try
            {
               

                    using (VENTACARROSContext db = new VENTACARROSContext())
                    {
                        var oUser = (from d in db.Usuarios
                                     where d.Usuario1 == oModel.Usuario1.Trim() || d.Email == oModel.Email.Trim()
                                     select d).FirstOrDefault();

                        if (oUser == null)
                        {
                        var pass = CheckPasswordStrength(oModel.Contraseña);
                        if (!string.IsNullOrEmpty(pass))
                        {
                            oRespuesta.Exito = 0;
                            oRespuesta.Mensaje = pass.ToString();
                        }
                        else { 


                        oPersona.Nombre = oModel.Nombre;
                            oPersona.Apellido = oModel.Apellido;
                            oPersona.Telefono = oModel.Telefono;
                            oPersona.Ocupacion = oModel.Ocupacion;
                            if (oModel.Sexo == "masculino")
                            {
                                oPersona.Sexo = 1;
                            }
                            if (oModel.Sexo == "femenino")
                            {
                                oPersona.Sexo = 2;
                            }
                            if (oModel.Sexo == "otro")
                            {
                                oPersona.Sexo = 3;
                            }
                            if (oModel.Imagen is not null)
                            {
                                oPersona.Imagen = _azureStorage.SaveFile(AzureContainers.USERS, oModel.Imagen);
                            }
                            oPersona.Direccion = oModel.Direccion;
                            oPersona.Edad = oModel.Edad;
                            db.Personas.Add(oPersona);
                            db.SaveChanges();

                            if (oModel.rol == "comprador")
                            {
                                oUsuario.IdRol = 1;
                            }
                            if (oModel.rol == "vendedor")
                            {
                                oUsuario.IdRol = 2;
                            }
                            oUsuario.Usuario1 = oModel.Usuario1;
                            oUsuario.Contraseña = Encrypt.GetSHA256(oModel.Contraseña);
                            oUsuario.Email = oModel.Email;
                            oUsuario.IdPersona = oPersona.Id;
                            db.Usuarios.Add(oUsuario);
                            db.SaveChanges();

                            oRespuesta.Exito = 1;
                            oRespuesta.Mensaje = "Usuario Registrado";
                            }
                        }
                        else
                        {
                            oRespuesta.Exito = 0;
                            oRespuesta.Mensaje = "usuario o contraseña ya registradas";
                        }
                    }
                
            }
            catch 
            {
                oRespuesta.Mensaje = "el formulario esta mal diligenciado";

            }
            return Ok(oRespuesta);

        }

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("La longitud mínima de la contraseña debe ser 8 carácteres" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]")
                && Regex.IsMatch(password, "[0-9]")))
                sb.Append("La contraseña debe ser alfanumérica" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("La contraseña debe contener caracteres especiales." + Environment.NewLine);
            return sb.ToString();
        }

        [HttpPut("send-reset-email/{email}")]
        public IActionResult SendEmail(string email)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            using (VENTACARROSContext db = new VENTACARROSContext())
            {
                var user = db.Usuarios.Where(a => a.Email == email).FirstOrDefault();
                if (user is null)
                {
                    oRespuesta.Exito = 0;
                    oRespuesta.Mensaje = "email no Existe";
                    //return NotFound(new
                    //{
                    //    StatusCode = 404,
                    //    Message= "email No esta registrado"
                    //});
                }
                else
                {
                    //var tokenBytes = RandomNumberGenerator.GetBytes(64);
                    var emailToken = Encrypt.GetSHA256(Guid.NewGuid().ToString());
                    user.TokenContraseña = emailToken;
                    user.ExpTokenContraseña = DateTime.Now.AddMinutes(15);
                    string from = _configuration["EmailSettings:From"];
                    var emailModel = new EmailModel(email, "Cambiar Contraseña!!", EmailBody.EmailStringBody(email, emailToken));
                    _emailServices.SendEmail(emailModel);
                    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Email enviado";
                }
            }
            return Ok(oRespuesta);
        }


        [HttpPut("reset-password")]
        public IActionResult ResetPassword(PasswordRequest passwordRequest)
        {
            var newToken = passwordRequest.emailToken.Replace(" ", "+");
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            using (VENTACARROSContext db = new VENTACARROSContext())
            {
                var user = db.Usuarios.Where(a => a.Email == passwordRequest.email).FirstOrDefault();
                if (user is null)
                {
                    oRespuesta.Exito = 0;
                    oRespuesta.Mensaje = "email no Existe";
                    //return NotFound(new
                    //{
                    //    StatusCode = 404,
                    //    Message= "email No esta registrado"
                    //});
                }
                var tokenCode = user.TokenContraseña;
                DateTime emailTokenExp = (DateTime)user.ExpTokenContraseña;
                if (tokenCode != passwordRequest.emailToken || emailTokenExp < DateTime.Now)
                {

                    oRespuesta.Exito = 0;
                    oRespuesta.Mensaje = "Link invalido";

                }
                else
                {
                    user.Contraseña = Encrypt.GetSHA256(passwordRequest.newPassword);
                    user.TokenContraseña = "";
                    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Contraseña Actualizada";
                }
            }
            return Ok(oRespuesta);

            }




    }
}
