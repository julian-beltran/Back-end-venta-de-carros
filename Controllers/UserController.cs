using CompraVentaCarrosApi.Models;
using CompraVentaCarrosApi.Models.Commons;
using CompraVentaCarrosApi.Models.Request;
using CompraVentaCarrosApi.Models.Response;
using CompraVentaCarrosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompraVentaCarrosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        public IAzureStorage _azureStorage;

        public UserController(IAzureStorage azureStorage)
        {
            _azureStorage = azureStorage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {

                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    var listUSer = db.Personas.OrderByDescending(d=>d.Id).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = listUSer;
                    
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);

        }

        [HttpPost]
        public IActionResult add(PersonaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext()) {
                    Persona oPersona = new Persona();
                    oPersona.Nombre = oModel.Nombre;
                    oPersona.Apellido = oModel.Apellido;
                    oPersona.Telefono = oModel.Telefono;
                    oPersona.Ocupacion = oModel.Ocupacion;
                    oPersona.Sexo = oModel.Sexo;
                    oPersona.Direccion = oModel.Direccion;
                    oPersona.Edad = oModel.Edad;
                    db.Personas.Add(oPersona);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);

        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, PersonaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Persona oPersona = db.Personas.Find(id);
                    oPersona.Nombre = oModel.Nombre;
                    oPersona.Apellido = oModel.Apellido;
                    oPersona.Telefono = oModel.Telefono;
                    oPersona.Ocupacion = oModel.Ocupacion;
                    oPersona.Sexo = oModel.Sexo;
                    oPersona.Direccion = oModel.Direccion;
                    oPersona.Edad = oModel.Edad;
                    db.Entry(oPersona).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Persona oPersona = db.Personas.Find(id);
                    db.Remove(oPersona);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);

        }




///////////////////////////////////////OFICIAL///////////////////////////////////////////


        [HttpGet ("{id}")]
        public IActionResult getPerson(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            usuarioRequest oUsuarioPersona = new usuarioRequest();
            oRespuesta.Exito = 0;

            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Usuario OUsuario = db.Usuarios.Find(id);
                    Persona Opersona = db.Personas.Find(OUsuario.IdPersona);
                    oUsuarioPersona.Nombre = Opersona.Nombre;
                    oUsuarioPersona.Apellido = Opersona.Apellido;
                    oUsuarioPersona.Telefono = Opersona.Telefono;
                    oUsuarioPersona.Ocupacion = Opersona.Ocupacion;
                    if(Opersona.Sexo ==1) { oUsuarioPersona.Sexo = "masculino"; };
                    if (Opersona.Sexo == 1) { oUsuarioPersona.Sexo = "femenino"; };
                    if (Opersona.Sexo == 1) { oUsuarioPersona.Sexo = "otro"; };
                    oUsuarioPersona.Direccion = Opersona.Direccion;
                    oUsuarioPersona.Edad = Opersona.Edad;
                    oUsuarioPersona.Usuario1 = OUsuario.Usuario1;
                    oUsuarioPersona.Contraseña = OUsuario.Contraseña;
                    oUsuarioPersona.Email = OUsuario.Email;
                    if (OUsuario.IdRol == 1) { oUsuarioPersona.rol = "comprador"; };
                    if (OUsuario.IdRol == 2) { oUsuarioPersona.rol = "vendedor"; };
                    if (OUsuario.IdRol == 3) { oUsuarioPersona.rol = "administrador"; };
                    oUsuarioPersona.Imagen = Opersona.Imagen;
                    oRespuesta.Data = oUsuarioPersona;
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "usuario traido";
                }
            }
            catch
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "ocurrio un error";

            }
            return Ok(oRespuesta);
        }














        //// GET: api/<UserController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
