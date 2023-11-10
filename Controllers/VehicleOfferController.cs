using Microsoft.AspNetCore.Mvc;
using CompraVentaCarrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompraVentaCarrosApi.Models.Request;
using CompraVentaCarrosApi.Models.Response;
using Microsoft.AspNetCore.Authorization;
using CompraVentaCarrosApi.Services;
using CompraVentaCarrosApi.Models.Commons;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompraVentaCarrosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class VehicleOfferController : ControllerBase
    {
        public IAzureStorage _azureStorage;

        public VehicleOfferController(IAzureStorage azureStorage)
        {
            _azureStorage = azureStorage;

        }


        // GET: api/<VehicleOfferController>
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    var listUSer = db.Vehiculos.Where(d => d.Aprovacion == true && d.EstadoCompra == false).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = listUSer;

                }

            }
            catch
            {

            }
            return Ok(oRespuesta);
        }




        [HttpPost]
        public IActionResult Add([FromForm] VehicleRequest vehicleRequest)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Vehiculo vehiculo = new Vehiculo();

                    vehiculo.IdPersona = vehicleRequest.IdPersona;
                    vehiculo.Carroseria = vehicleRequest.Carroseria;
                    vehiculo.Kilometraje = vehicleRequest.Kilometraje;
                    vehiculo.Linea = vehicleRequest.Linea;
                    vehiculo.Marca = vehicleRequest.Marca;
                    vehiculo.Modelo = vehicleRequest.Modelo;
                    vehiculo.Placa = vehicleRequest.Placa;
                    vehiculo.Precio = vehicleRequest.Precio;
                    vehiculo.EstadoCompra = false;
                    vehiculo.Fecha = DateTime.Now;
                    if (vehicleRequest.Imagen1 is not null)
                    {
                        vehiculo.Imagen1 = _azureStorage.SaveFile(AzureContainers.USERS, vehicleRequest.Imagen1);
                    }
                    if (vehicleRequest.Imagen2 is not null)
                    {
                        vehiculo.Imagen2 = _azureStorage.SaveFile(AzureContainers.USERS, vehicleRequest.Imagen2);
                    }
                    if (vehicleRequest.Imagen3 is not null)
                    {
                        vehiculo.Imagen3 = _azureStorage.SaveFile(AzureContainers.USERS, vehicleRequest.Imagen3);
                    }
                    vehiculo.Descripcion = vehicleRequest.Descripcion;
                    vehiculo.Aprovacion = false;
                    var usuario = db.Usuarios.Find(vehicleRequest.IdPersona);
                    var idPersona = db.Personas.Find(usuario.IdPersona);
                    vehiculo.IdPersona = idPersona.Id;
                    db.Vehiculos.Add(vehiculo);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Oferta enviada al administrador";
                }

            }
            catch
            {
                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "llene de manera correcta el formulario";

            }

            return Ok(oRespuesta);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, Aprovacion aprovacion)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Vehiculo vehiculo = db.Vehiculos.Find(id);
                    vehiculo.Aprovacion = aprovacion.aprovacion;
                    db.Entry(vehiculo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "La oferta ha sido aprobada";
                }
            }
            catch
            {
                oRespuesta.Mensaje = "ocurrio un error del sistema";

            }
            return Ok(oRespuesta);
        }


        [HttpPut("estadoCompra/{id}")]
        public IActionResult PutEstadoCompra(int id, EstadoCompra estado)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Vehiculo vehiculo = db.Vehiculos.Find(id);
                    vehiculo.EstadoCompra = estado.estadoCompra;
                    db.Entry(vehiculo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "La oferta ha sido aprobada";
                }
            }
            catch
            {
                oRespuesta.Mensaje = "ocurrio un error del sistema";

            }
            return Ok(oRespuesta);
        }




        // GET api/<VehicleOfferController>/5
        [HttpGet("getOffers")]
        public IActionResult GetOffers()
        {

            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            List<GetOffers> getOffers = null;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    getOffers = (from d in db.Vehiculos join
                                 c in db.Personas on d.IdPersona equals c.Id
                                 where d.Aprovacion == false
                                 select new GetOffers
                                 {
                                     IdVehicle = d.Id,
                                     Aprovacion = d.Aprovacion,
                                     Nombre = c.Nombre,
                                     Apellido = c.Apellido,
                                     Telefono = c.Telefono,
                                     Direccion = c.Direccion,
                                     Imagen = c.Imagen,
                                     IdPersona = c.Id

                                 }).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Lista de ofertas no aprovadas";
                    oRespuesta.Data = getOffers;
                }

            }
            catch
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "ocurrio un error";
            }
            return Ok(oRespuesta);

        }

        // GET ofertas aprovadas
        [HttpGet("getOffersApprove")]
        public IActionResult GetOffersApprove()
        {

            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            List<GetOffers> getOffers = null;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    getOffers = (from d in db.Vehiculos
                                 join
          c in db.Personas on d.IdPersona equals c.Id
                                 where d.Aprovacion == true
                                 select new GetOffers
                                 {
                                     IdVehicle = d.Id,
                                     Aprovacion = d.Aprovacion,
                                     Nombre = c.Nombre,
                                     Apellido = c.Apellido,
                                     Telefono = c.Telefono,
                                     Direccion = c.Direccion,
                                     Imagen = c.Imagen,
                                     IdPersona = c.Id

                                 }).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Lista de ofertas aprovadas";
                    oRespuesta.Data = getOffers;
                }

            }
            catch
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "ocurrio un error";
            }
            return Ok(oRespuesta);

        }


        [HttpGet("offer/{id}")]
        public IActionResult GetOneOffer(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            GetOffer getoffer = new GetOffer();
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    var oferta = db.Vehiculos.Find(id);
                    var persona = db.Personas.Find(oferta.IdPersona);
                    var usuario = db.Usuarios.Where(a => a.IdPersona == persona.Id).FirstOrDefault();

                    getoffer.IdVehicle = oferta.Id;
                    getoffer.Aprovacion = oferta.Aprovacion;
                    getoffer.EstadoCompra = oferta.EstadoCompra;
                    getoffer.Nombre = persona.Nombre;
                    getoffer.Apellido = persona.Apellido;
                    getoffer.Telefono = persona.Telefono;
                    getoffer.Direccion = persona.Direccion;
                    getoffer.Imagen = persona.Imagen;
                    getoffer.IdPersona = persona.Id;
                    getoffer.Marca = oferta.Marca;
                    getoffer.Linea = oferta.Linea;
                    getoffer.Modelo = oferta.Modelo;
                    getoffer.Carroseria = oferta.Carroseria;
                    getoffer.Kilometraje = oferta.Kilometraje;
                    getoffer.Placa = oferta.Placa;
                    getoffer.Precio = oferta.Precio;
                    getoffer.Imagen1 = oferta.Imagen1;
                    getoffer.Imagen2 = oferta.Imagen2;
                    getoffer.Imagen3 = oferta.Imagen3;
                    getoffer.Descripcion = oferta.Descripcion;
                    getoffer.correo = usuario.Email;
                    getoffer.Fecha = oferta.Fecha;
                    if (usuario.IdRol == 1) { getoffer.rol = "comprador"; }
                    if (usuario.IdRol == 2) { getoffer.rol = "vendedor"; }


                    oRespuesta.Data = getoffer;
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "oferta obtenida";

                }
            }
            catch
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "ocurrio un error en el sistema";
            }
            return Ok(oRespuesta);

        }




        [HttpGet("imagen/{id}")]
        public IActionResult GetImagenOffer(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            LinkedList<imagenResponse> list = new LinkedList<imagenResponse>();
            imagenResponse Imagen1 = new imagenResponse();
            imagenResponse Imagen2 = new imagenResponse();
            imagenResponse Imagen3 = new imagenResponse();
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    var oferta = db.Vehiculos.Find(id);

                    Imagen1.imagen = oferta.Imagen1;
                    list.AddFirst(Imagen1);

                    Imagen2.imagen = oferta.Imagen2;
                    list.AddFirst(Imagen2);

                    Imagen3.imagen = oferta.Imagen3;
                    list.AddFirst(Imagen3);

                    oRespuesta.Data = list;
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "oferta obtenida";

                }
            }
            catch
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "ocurrio un error en el sistema";
            }
            return Ok(oRespuesta);

        }

        [HttpPost("Mensaje")]
        public IActionResult Messsage(MessageRequest oMessage)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Mensaje mensaje = new Mensaje();

                    mensaje.IdPersona = oMessage.IdPersona;
                    mensaje.Nombre = oMessage.Nombre;
                    mensaje.Apellido = oMessage.Apellido;
                    mensaje.Email = oMessage.Email;
                    mensaje.Telefono = oMessage.Telefono;
                    mensaje.Pregunta = oMessage.Pregunta;
                    mensaje.IdVehiculo = oMessage.IdVehiculo;

                    db.Mensajes.Add(mensaje);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "El mensaje ha sido enviado exitosamente";
                }
            }
            catch
            {
                oRespuesta.Mensaje = "ah ocurrido un error en el sistema";
            }
            return Ok(oRespuesta);
        }

        [HttpGet("GetMensajes/{id}")]
        public IActionResult getMessages(int id)
        {
           

            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            List<getMessage> lst = new List<getMessage>();
           try
            {

                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Usuario OUsuario = db.Usuarios.Find(id);
                    Persona Opersona = db.Personas.Find(OUsuario.IdPersona);



                    lst = (from d in db.Mensajes
                           where d.IdPersona == Opersona.Id
                           select new getMessage
                           {
                               Id=d.Id,
                               Nombre =d.Nombre,
                               Apellido = d.Apellido,
                               Email = d.Email,
                               Telefono = d.Telefono,
                               Pregunta = d.Pregunta,
                               IdPersona =d.IdPersona,
                               IdVehiculo = d.IdVehiculo
                           }).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;

                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);
        }


        // DELETE api/<VehicleOfferController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



    }
}
